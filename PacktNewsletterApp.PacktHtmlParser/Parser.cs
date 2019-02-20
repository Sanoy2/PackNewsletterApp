using System.Collections.Generic;
using System.Linq;
using Fizzler.Systems.HtmlAgilityPack;
using HtmlAgilityPack;
using Newtonsoft.Json;
using PacktNewsletterApp.Data.Models;
using PacktNewsletterApp.PacktHtmlParser.Interfaces;

namespace PacktNewsletterApp.PacktHtmlParser
{
    public class Parser : IParser
    {
        private readonly IJsonGetter jsonGetter;

        public Parser()
        {
            // jsonGetter = new JsonGetterMock();
            jsonGetter = new JsonGetter();
        }

        public Ebook Parse()
        {
            var ebook = new Ebook();

            var json = jsonGetter.GetEbookDescription();

            var fullEbookDescription = JsonConvert.DeserializeObject<FullEbookDescription>(json);

            ebook.Title = fullEbookDescription.title;
            ebook.CoverUrl = fullEbookDescription.coverImage;

            var description = ParseFeatures(fullEbookDescription.features);

            ebook.Description = description;

            return ebook;
        }

        private List<string> ParseFeatures(string features)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(features);
            var liList = doc.DocumentNode.QuerySelectorAll("li");

            return liList.Select(n => n.InnerText).ToList();
;        }
    }
}