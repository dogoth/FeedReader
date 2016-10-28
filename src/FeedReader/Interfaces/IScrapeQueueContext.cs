using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServer.Interfaces
{
    public interface IScrapeQueueContext
    {
        void Push(NewsServer.Model.ScrapeQueue queueItem);

        NewsServer.Model.ScrapeQueue Pop();

        NewsServer.Model.ScrapeQueue Peek();
    }
}
