// Services/HtmlParser.cs
using HtmlAgilityPack;

namespace WebCrawler.Api.Services
{
    public static class HtmlParser
    {
        public static IEnumerable<string> ExtractLinks(string html, Uri baseUri)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var links = doc.DocumentNode.SelectNodes("//a[@href]")?
                .Select(a => a.GetAttributeValue("href", ""))
                .Where(href => !string.IsNullOrEmpty(href))
                .Select(href => new Uri(baseUri, href).AbsoluteUri);

            return links ?? Enumerable.Empty<string>();
        }
    }
}
