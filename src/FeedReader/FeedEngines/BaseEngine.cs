using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Xml.Linq;
using NewsServer.Model;

namespace NewsServer.FeedEngines
{
    public abstract class BaseEngine : NewsServer.Interfaces.IFeedEngine
    {
        public BaseEngine (PublicationSection publicationSection, Publication publication, NewsCrud_DBContext dbContext)
        {
            _publicationSection = publicationSection;
            _publication = publication;
            _dbContext = dbContext;
        }

        protected string _rawFeedBody;

        protected Model.NewsCrud_DBContext _dbContext;

        protected Model.PublicationSection _publicationSection;

        protected Model.Publication _publication;

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

        public Model.PublicationSection publicationSection
        {
            get
            {
                return _publicationSection;
            }
        }

        public Model.Publication publication
        {
            get
            {
                return _publication;
            }
        }

        public void ProcessFeed()
        {
            if (_publicationSection == null || String.IsNullOrEmpty(_publicationSection.Url) == true)
            {
                throw new ArgumentException("feed URL cannot be null");
            }

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage resp = client.GetAsync(new Uri(publicationSection.Url)).Result;
                if (resp.IsSuccessStatusCode == true)
                {
                    _rawFeedBody = resp.Content.ReadAsStringAsync().Result;
                }
            }

            parseRawBody();

            _publicationSection.LastScraped = DateTimeOffset.Now;
            _dbContext.Update(_publicationSection);
            _dbContext.SaveChanges();
        }

        protected abstract void parseRawBody();
    }
}
