using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedReader.Model;

namespace FeedReader.Controllers
{
    [Route("api/[controller]")]
    public class FeedProcessorController : Controller
    {
        private NewsCrud_DBContext _dbContext;
        private Interfaces.IScrapeQueueContext _queueContext; 

        public FeedProcessorController(NewsCrud_DBContext dbContext, Interfaces.IScrapeQueueContext queueContext)
        {
            _dbContext = dbContext;
            _queueContext = queueContext;
        }

        [HttpGet]
        // GET: /<controller>/
        public string Get()
        {
            //return View();
            return "default api endpoint";
        }

        [HttpGet("ProcessFeeds")]
        // GET: /<controller>/ProcessFeeds
        public string ProcessFeeds()
        {
            List<Publication> scrapeEnabledPubs = _dbContext.Publication.Where(p => p.ScrapeEnabled == true).ToList();


            List<Interfaces.IFeedEngine> feedEngines = new List<Interfaces.IFeedEngine>();
            foreach(Publication p in scrapeEnabledPubs)
            {
                List<PublicationSection> sections = _dbContext.PublicationSection.Where(ps => ps.PublicationId == p.Id && ps.Enabled == true).ToList();
                foreach(PublicationSection ps in sections)
                {
                    switch (p.Code)
                    {
                        case "SMH":
                            {
                                feedEngines.Add(new FeedEngines.SMH_RSSEngine(ps, p, _dbContext));
                                break;
                            }
                        case "Telegraph":
                            {
                                feedEngines.Add(new FeedEngines.Telegraph_RSSEngine(ps, p, _dbContext));
                                break;
                            }
                    }
                }
            }

            int queueTotal = 0;

            foreach (Interfaces.IFeedEngine feed in feedEngines)
            {
                feed.ProcessFeed();
                foreach (ScrapeQueue q in feed.queueItems)
                {
                    _queueContext.Push(q);
                }

                queueTotal += feed.queueItems.Count;
            }

            //DB CONTEXT PASSING NOT THREAD SAFE, NEED TO FIX Save in ProcessFeed before making parallel
            //Parallel.ForEach(feedEngines,
            //    (feed) =>
            //     {
            //         feed.ProcessFeed();
            //     }
            //);

            return queueTotal.ToString();
        }

      
    }
}
