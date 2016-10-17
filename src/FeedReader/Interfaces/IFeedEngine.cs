using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeedReader.Interfaces
{
    public interface IFeedEngine
    {
        string rawFeedBody { get; }

        Model.PublicationSection publicationSection { get; }

        Model.Publication publication { get; }

        List<Model.ScrapeQueue> queueItems { get; }

        void ProcessFeed();
    }
}
