using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedReader.Interfaces
{
    public interface IFeedEngine
    {
        string rawFeedBody { get; }

        Model.PublicationSection section { get; }

        List<Model.ScrapeQueue> queueItems { get; }

        void ProcessFeed();
    }
}
