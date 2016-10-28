using System;
using System.Collections.Generic;

namespace NewsServer.Model
{
    public partial class PublicationSection
    {
        public int Id { get; set; }
        public int? PublicationId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public bool? Enabled { get; set; }
        public DateTimeOffset? LastScraped { get; set; }
        public int? Sequence { get; set; }

        public virtual Publication Publication { get; set; }
    }
}
