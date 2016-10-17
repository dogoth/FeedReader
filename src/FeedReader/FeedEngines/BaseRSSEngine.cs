using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;

namespace FeedReader.FeedEngines
{
    public abstract class BaseRSSEngine : FeedReader.Interfaces.IFeedEngine
    {
        public BaseRSSEngine(Model.PublicationSection section, Model.NewsCrud_DBContext dbContext)
        {
            _section = section;
            _dbContext = dbContext;
        }

        protected string _rawFeedBody;

        protected Model.NewsCrud_DBContext _dbContext;

        protected Model.PublicationSection _section;

        protected List<Model.ScrapeQueue> _queueItems;

        public string rawFeedBody
        {
            get
            {
                return _rawFeedBody;
            }
        }

        public List<Model.ScrapeQueue> queueItems
        {
            get
            {
                return _queueItems;
            }
        }

        public Model.PublicationSection section
        {
            get
            {
                return _section;
            }
        }

        public void ProcessFeed()
        {
            if (_section == null || String.IsNullOrEmpty(_section.Url) == true)
            {
                throw new ArgumentException("feed URL cannot be null");
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = client.GetAsync(new Uri(section.Url)).Result;
                if (resp.IsSuccessStatusCode == true)
                {
                    _rawFeedBody = resp.Content.ReadAsStringAsync().Result;
                }
            }

            parseRawBody();

            _section.LastScraped = DateTimeOffset.Now;
            _dbContext.Update(section);
            _dbContext.SaveChanges();
        }

        protected abstract void parseRawBody();
    }
}
