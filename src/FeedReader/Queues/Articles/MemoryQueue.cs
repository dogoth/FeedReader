using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NewsServer.Model;

namespace NewsServer.Queues.Articles
{
    public class MemoryQueue : Interfaces.IArticleQueueContext
    {
        private Queue<string> q = new Queue<string>();
        public string Peek()
        {
            return q.Peek();
        }

        public string Pop()
        {
            return q.Dequeue();
        }

        public void Push(string queueItem)
        {
            q.Enqueue(queueItem);
        }
    }
}
