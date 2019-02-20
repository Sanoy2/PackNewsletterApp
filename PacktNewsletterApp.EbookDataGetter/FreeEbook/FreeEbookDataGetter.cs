using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Newtonsoft.Json;
using PacktNewsletterApp.EbookDataGetter.Models;
using PacktNewsletterApp.Data.Models;


namespace PacktNewsletterApp.EbookDataGetter.FreeEbook
{
    public class FreeEbookDataGetter : IEbookDataGetter
    {
        public Ebook GetEbook()
        {
            return GetEbookAsync().Result;
        }

        public async Task<Ebook> GetEbookAsync()
        {
            var fullEbookDescription = await GetWholeEbookData();

            var ebook = new Ebook()
            {
                Title = fullEbookDescription.title,
                CoverUrl = fullEbookDescription.coverImage,
                Description = ParseFeatures(fullEbookDescription.features)
            };

            return ebook;
        }

        private async Task<FullEbookData> GetWholeEbookData()
        {
            var fullEbookGetter = new FullEbookGetter();

            var json = await fullEbookGetter.GetFullEbookAsJSON();

            var fullEbookDescription = JsonConvert.DeserializeObject<FullEbookData>(json);

            return fullEbookDescription;
        }

        private List<string> ParseFeatures(string features)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(features);
            var liList = doc.DocumentNode.QuerySelectorAll("li");

            return liList.Select(n => n.InnerText).ToList();
        }
    }
}