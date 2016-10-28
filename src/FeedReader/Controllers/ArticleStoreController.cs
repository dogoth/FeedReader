using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FeedReader.Model;

namespace FeedReader.Controllers
{
    [Route("api/[controller]")]
    public class ArticleStore : Controller
    {
        private NewsCrud_DBContext _dbContext;
        private Interfaces.IArticleQueueContext _queueContext;

        public ArticleStore(NewsCrud_DBContext dbContext, Interfaces.IArticleQueueContext queueContext)
        {
            _dbContext = dbContext;
            _queueContext = queueContext;
        }

        [HttpGet]
        // GET: /<controller>/
        public string Get()
        {
            //fetch up to 20 each run
            int i = 0;

            while (i < 20)
            {
                string articleJsonString = _queueContext.Pop();
                if(String.IsNullOrEmpty(articleJsonString) == true)
                {
                    //nothing found, can do 'break' here if don't want to try 20 more times
                    //but maybe they are empty 'errored' messages that need to be popped
                }
                else
                {
                    dynamic articleJson = null;
                    try
                    {
                        articleJson = Newtonsoft.Json.JsonConvert.DeserializeObject(articleJsonString);
                    }
                    catch(Exception ex)
                    {
                        string exception = ex.ToString();
                        //gulp
                    }

                    if(articleJson != null)
                    {
                        //try parsing all the bits
                        try
                        {
                            Author a1 = new Author();
                            a1.FindByName("");
                            a1.Name = "test 1";

                            Author a2 = new Author();
                            a2.Name = "test 2";

                            ArticleData art = new ArticleData();
                            art.Body = "art test";


                            AuthorArticle a = new AuthorArticle();

                            //try get pub and section info from input
                            Publication p = new Publication();

                            //otherwise create new publication and/or section
                            PublicationSection psect = new PublicationSection();

                        }
                        catch (Exception ex)
                        {
                            string exception = ex.ToString();
                            //gulp
                        }

                    }
                       

                }
            }

            //return View();
            return "ingest api done";
        }
    }
}
