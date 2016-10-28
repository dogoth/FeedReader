using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsServer.Model;

namespace NewsServer.Queues.Scrape
{
    public class MemoryQueue : Interfaces.IScrapeQueueContext
    {
        private Queue<Model.ScrapeQueue> q = new Queue<ScrapeQueue>();
        public ScrapeQueue Peek()
        {
            return q.Peek();
        }

        public ScrapeQueue Pop()
        {
            return q.Dequeue();
        }

        public void Push(ScrapeQueue queueItem)
        {
            q.Enqueue(queueItem);
        }
    }
}
