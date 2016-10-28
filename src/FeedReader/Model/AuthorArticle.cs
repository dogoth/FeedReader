using System;
using System.Collections.Generic;

namespace FeedReader.Model
{
    public partial class AuthorArticle
    {
        public int AuthorId { get; set; }
        public int ArticleId { get; set; }

        public virtual ArticleData Article { get; set; }
        public virtual Author Author { get; set; }
    }
}
