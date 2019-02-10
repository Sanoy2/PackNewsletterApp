using System;
using System.Collections.Generic;
using System.Text;

namespace PacktNewsletterApp.PacktHtmlParser
{
    public class TodaysFreeEbook
    {
        public List<Data> data { get; set; }
        public int count { get; set; }
    }

    public class Data
    {
        public string id { get; set; }
        public string productId { get; set; }
        public DateTime availableFrom { get; set; }
        public DateTime expiresAt { get; set; }
        public bool limitedAmount { get; set; }
        public object amountAvailable { get; set; }
        public object details { get; set; }
        public int priority { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public object deletedAt { get; set; }
    }
}
