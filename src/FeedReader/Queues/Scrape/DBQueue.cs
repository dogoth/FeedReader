using FeedReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedReader.Interfaces;

namespace FeedReader.Queues.Scrape
{
    public class DBQueue: Interfaces.IScrapeQueueContext
    {
        private NewsCrud_DBContext _context;

        public DBQueue(NewsCrud_DBContext context)
        {
            _context = context;
        }

        ScrapeQueue IScrapeQueueContext.Peek()
        {
            throw new NotImplementedException();
        }

        ScrapeQueue IScrapeQueueContext.Pop()
        {
            throw new NotImplementedException();
        }

        void IScrapeQueueContext.Push(ScrapeQueue queueItem)
        {
            _context.ScrapeQueue.Add(queueItem);
            _context.SaveChanges();
        }
    }
}
