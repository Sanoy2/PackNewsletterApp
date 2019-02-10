using System;
using System.Collections.Generic;

namespace PacktNewsletterApp.PacktHtmlParser
{
    public class FullEbookDescription
    {
        public string title { get; set; }
        public string type { get; set; }
        public string coverImage { get; set; }
        public string productId { get; set; }
        public string isbn13 { get; set; }
        public string oneLiner { get; set; }
        public int pages { get; set; }
        public DateTime publicationDate { get; set; }
        public string length { get; set; }
        public string about { get; set; }
        public string learn { get; set; }
        public string features { get; set; }
        public List<string> authors { get; set; }
        public string shopUrl { get; set; }
        public string readUrl { get; set; }
        public string category { get; set; }
        public bool earlyAccess { get; set; }
        public bool available { get; set; }
    }
}