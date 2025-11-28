using Microsoft.EntityFrameworkCore;
using WebCrawler.Api.Data;
using WebCrawler.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// SQLite for simplicity
builder.Services.AddDbContext<CrawlDbContext>(opt =>
    opt.UseSqlite("Data Source=crawler.db"));

builder.Services.AddSingleton<ProducerConsumerQueue>();
builder.Services.AddHostedService<CrawlerService>();

builder.Services.AddControllers();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CrawlDbContext>();
    db.Database.EnsureCreated();
}

app.MapControllers();
app.Run();
