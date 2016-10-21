using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedReader.Interfaces
{
    public interface IScrapeQueueContext
    {
        void Push(FeedReader.Model.ScrapeQueue queueItem);

        FeedReader.Model.ScrapeQueue Pop();

        FeedReader.Model.ScrapeQueue Peek();
    }
}
