using FeedReader.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeedReader.Interfaces;

namespace FeedReader.Queue
{
    public class DBQueue: Interfaces.IQueueContext
    {
        private NewsCrud_DBContext _context;

        public DBQueue(NewsCrud_DBContext context)
        {
            _context = context;
        }

        ScrapeQueue IQueueContext.Peek()
        {
            throw new NotImplementedException();
        }

        ScrapeQueue IQueueContext.Pop()
        {
            throw new NotImplementedException();
        }

        void IQueueContext.Push(ScrapeQueue queueItem)
        {
            _context.ScrapeQueue.Add(queueItem);
            _context.SaveChanges();
        }
    }
}
