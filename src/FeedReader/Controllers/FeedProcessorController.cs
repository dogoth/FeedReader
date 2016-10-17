using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedReader.Model;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FeedReader.Controllers
{
    [Route("api/[controller]")]
    public class FeedProcessorController : Controller
    {
        private NewsCrud_DBContext _context; 

        public FeedProcessorController(NewsCrud_DBContext context)
        {
            _context = context;
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
            List<Publication> scrapeEnabledPubs = _context.Publication.Where(p => p.ScrapeEnabled == true).ToList();

            string s = "";
            List<Interfaces.IFeedEngine> feedEngines = new List<Interfaces.IFeedEngine>();
            foreach(Publication p in scrapeEnabledPubs)
            {
                List<PublicationSection> sections = _context.PublicationSection.Where(ps => ps.PublicationId == p.Id && ps.Enabled == true).ToList();
                foreach(PublicationSection ps in sections)
                {
                    switch (p.Code)
                    {
                        case "SMH":
                            {
                                feedEngines.Add(new FeedEngines.SMH_RSSEngine(ps, _context));
                                break;
                            }
                        case "Telegraph":
                            {
                                feedEngines.Add(new FeedEngines.Telegraph_RSSEngine(ps, _context));
                                break;
                            }
                    }
                }
            }

            int queueTotal = 0;

            foreach (Interfaces.IFeedEngine feed in feedEngines)
            {
                feed.ProcessFeed();
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
