using System;
using System.Collections.Generic;

namespace FeedReader.Model
{
    public partial class ScrapeQueue
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string IdHash { get; set; }
        public DateTimeOffset? DateAdded { get; set; }
        public DateTimeOffset? DateActioned { get; set; }
        public string ScrapeOutcome { get; set; }
        public string ScrapeErrors { get; set; }
        public string CallbackUrl { get; set; }
        public string PublicationCode { get; set; }
        public int? PublicationId { get; set; }
        public int? PublicationSectionId { get; set; }
    }
}
