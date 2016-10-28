using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewsServer.Interfaces
{
    /*
 {
 "URL": "http://www.smh.com.au/world/us-election/when-its-all-about-donald-whats-the-risk-to-trump-inc-20161017-gs4kki.html”,
 "Authors": [
   "Michael Evans"
 ], 
 "Text": "As Donald Trump's campaign to become US President snowballed and gathered an almost inexorable momentum over the past...”, 
 "Title": "When it's all about Donald, what's the risk to Trump Inc?", 
 "media": [],
 "dateScraped": "",
 "idHash": "",
 "publicationID",
 "publicationSectionID"
}
*/
    public interface IArticleQueueContext
    {
        void Push(string queueItem);

        string Pop();

        string Peek();
    }
}
