// Services/ProducerConsumerQueue.cs
using System.Collections.Concurrent;

namespace WebCrawler.Api.Services
{
    public class ProducerConsumerQueue
    {
        private readonly BlockingCollection<Uri> _queue = new();

        public void Enqueue(Uri uri) => _queue.Add(uri);

        public Uri Dequeue(CancellationToken cancellationToken)
            => _queue.Take(cancellationToken);

        public int Count => _queue.Count;
    }
}
