using System;
using System.Collections.Generic;

namespace NewsServer.Model
{
    public partial class Author
    {
        public Author()
        {
            AuthorArticle = new HashSet<AuthorArticle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Country { get; set; }

        public virtual ICollection<AuthorArticle> AuthorArticle { get; set; }
    }
}
