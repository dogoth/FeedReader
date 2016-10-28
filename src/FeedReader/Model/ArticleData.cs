using System;
using System.Collections.Generic;

namespace NewsServer.Model
{
    public partial class ArticleData
    {
        public ArticleData()
        {
            AuthorArticle = new HashSet<AuthorArticle>();
        }

        public int Id { get; set; }
        public int? PublicationId { get; set; }
        public int? PublicationSectionId { get; set; }
        public string Url { get; set; }
        public string IdHash { get; set; }
        public DateTimeOffset? PublishDate { get; set; }
        public DateTimeOffset? FirstScrapeDate { get; set; }
        public DateTimeOffset? LastScrapeDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }

        public virtual ICollection<AuthorArticle> AuthorArticle { get; set; }
        public virtual ArticleData IdNavigation { get; set; }
        public virtual ArticleData InverseIdNavigation { get; set; }
        public virtual Publication Publication { get; set; }
    }
}
