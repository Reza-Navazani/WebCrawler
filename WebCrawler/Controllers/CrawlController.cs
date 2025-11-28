// Controllers/CrawlController.cs
using Microsoft.AspNetCore.Mvc;
using WebCrawler.Api.Services;

namespace WebCrawler.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrawlController : ControllerBase
    {
        private readonly ProducerConsumerQueue _queue;

        public CrawlController(ProducerConsumerQueue queue)
        {
            _queue = queue;
        }

        [HttpPost("start")]
        public IActionResult Start([FromBody] string url)
        {
            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
                return BadRequest("Invalid URL");

            _queue.Enqueue(uri);
            return Ok($"Crawling started for {url}");
        }

        [HttpGet("status")]
        public IActionResult Status()
        {
            return Ok(new { QueueSize = _queue.Count });
        }
    }
}
