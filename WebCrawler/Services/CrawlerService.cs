// Services/CrawlerService.cs
using System.Net.Http;
using WebCrawler.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace WebCrawler.Api.Services
{
    public class CrawlerService : BackgroundService
    {
        private readonly ProducerConsumerQueue _queue;
        private readonly IServiceProvider _services;
        private readonly HttpClient _httpClient = new();
        private readonly HashSet<string> _visited = new();

        public CrawlerService(ProducerConsumerQueue queue, IServiceProvider services)
        {
            _queue = queue;
            _services = services;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Start multiple consumer threads
            var workers = Enumerable.Range(0, 4) // 4 threads
                .Select(_ => Task.Run(() => Consume(stoppingToken), stoppingToken));

            await Task.WhenAll(workers);
        }

        private async Task Consume(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                Uri uri;
                try
                {
                    uri = _queue.Dequeue(token);
                }
                catch (OperationCanceledException) { break; }

                if (!_visited.Add(uri.AbsoluteUri)) continue; // skip if already visited

                try
                {
                    var html = await _httpClient.GetStringAsync(uri, token);

                    // Save to DB
                    using var scope = _services.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<CrawlDbContext>();
                    db.Pages.Add(new PageResult
                    {
                        Url = uri.AbsoluteUri,
                        HtmlContent = html,
                        CrawledAt = DateTime.UtcNow
                    });
                    await db.SaveChangesAsync(token);

                    // Enqueue new links
                    foreach (var link in HtmlParser.ExtractLinks(html, uri))
                    {
                        if (Uri.TryCreate(link, UriKind.Absolute, out var newUri))
                            _queue.Enqueue(newUri);
                    }
                }
                catch
                {
                    // Ignore bad requests
                }
            }
        }
    }
}
