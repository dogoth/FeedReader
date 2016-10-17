using System;
using System.Collections.Generic;
using System.Linq;
using FeedReader.Model;
using System.Xml.Linq;

namespace FeedReader.FeedEngines
{
    public class SMH_RSSEngine: BaseRSSEngine
    {
        public SMH_RSSEngine(PublicationSection section, NewsCrud_DBContext dbContext) : base(section, dbContext)
        {
        }

        override protected void parseRawBody()
        {
            XDocument doc = XDocument.Parse(rawFeedBody);

            var entries = from item in doc.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                          select new Model.ScrapeQueue()
                          {
                              Url = item.Elements().First(i => i.Name.LocalName == "link").Value,
                              DateAdded = DateTimeOffset.Now,
                              IdHash = item.Elements().First(i => i.Name.LocalName == "title").Value
                          };

            _queueItems = entries.ToList();

        }
    }
}
