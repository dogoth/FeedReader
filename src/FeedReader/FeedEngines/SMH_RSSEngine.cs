using System;
using System.Collections.Generic;
using System.Linq;
using NewsServer.Model;
using System.Xml.Linq;

namespace NewsServer.FeedEngines
{
    public class SMH_RSSEngine: BaseEngine
    {
        public SMH_RSSEngine(PublicationSection publicationSection, Publication publication, NewsCrud_DBContext dbContext) : base(publicationSection, publication, dbContext)
        {
        }

        override protected void parseRawBody()
        {
            XDocument doc = XDocument.Parse(rawFeedBody);

            var entries = from item in doc.Descendants().First(i => i.Name.LocalName == "channel").Elements().Where(i => i.Name.LocalName == "item")
                          select new Model.ScrapeQueue()
                          {
                              Url = item.Elements().First(i => i.Name.LocalName == "link").Value,
                              IdHash = Util.Tools.CreateIDHash(item.Elements().First(i => i.Name.LocalName == "title").Value, item.Elements().First(i => i.Name.LocalName == "link").Value),
                              DateAdded = DateTimeOffset.Now,
                              PublicationCode = publication.Code,
                              PublicationId = publication.Id,
                              PublicationSectionId = publicationSection.Id
                          };

            _queueItems = entries.ToList();

        }
    }
}
