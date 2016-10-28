using System;
using System.Collections.Generic;

namespace NewsServer.Model
{
    public partial class Publication
    {
        public Publication()
        {
            ArticleData = new HashSet<ArticleData>();
            PublicationSection = new HashSet<PublicationSection>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Url { get; set; }
        public bool? ScrapeEnabled { get; set; }
        public bool? DisplayEnabled { get; set; }

        public virtual ICollection<ArticleData> ArticleData { get; set; }
        public virtual ICollection<PublicationSection> PublicationSection { get; set; }
    }
}
