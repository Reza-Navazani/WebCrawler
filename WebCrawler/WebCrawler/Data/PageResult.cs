// Data/PageResult.cs
namespace WebCrawler.Api.Data
{
    public class PageResult
    {
        public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public string HtmlContent { get; set; } = string.Empty;
        public DateTime CrawledAt { get; set; } = DateTime.UtcNow;
    }
}
