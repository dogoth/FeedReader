using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedReader.Model;

namespace FeedReader.Queue
{
    public class MemoryQueue : Interfaces.IQueueContext
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
