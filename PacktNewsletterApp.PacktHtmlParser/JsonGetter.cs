using System;
using System.Linq;
using System.Net.Http;
using Newtonsoft.Json;
using PacktNewsletterApp.PacktHtmlParser.Interfaces;

namespace PacktNewsletterApp.PacktHtmlParser
{
    public class JsonGetter : IJsonGetter
    {
        public string GetEbookDescription()
        {
            var ebookId = GetId();
            var urlForDescription = UrlForDescription(ebookId);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(urlForDescription).Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    return responseBody;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nException Caught while getting description!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return "";
                }
            }
        }

        private string GetId()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync(UrlForId()).Result;
                    response.EnsureSuccessStatusCode();
                    string responseBody = response.Content.ReadAsStringAsync().Result;

                    string productId = JsonConvert.DeserializeObject<TodaysFreeEbook>(responseBody).data.FirstOrDefault().productId;

                    Console.WriteLine($"Got ebook product id: {productId}");
                    return productId;
                }
                catch (Exception e)
                {
                    Console.WriteLine("\nException Caught while getting product id!");
                    Console.WriteLine("Message :{0} ", e.Message);
                    return "";
                }
            }
        }

        private string UrlForId()
        {
            //string example =
            //    "https://services.packtpub.com/free-learning-v1/offers?dateFrom=2019-02-08T00:00:00.000Z&dateTo=2019-02-09T00:00:00.000Z";
            var today = FormatDateTime(DateTime.Today);
            var tommorow = FormatDateTime(DateTime.Today.AddDays(1));

            string url =
                $"https://services.packtpub.com/free-learning-v1/offers?dateFrom={today}&dateTo={tommorow}";

            return url;
        }

        private string FormatDateTime(DateTime dateTime)
        {
            var day = NumberFormat(dateTime.Day);
            var month = NumberFormat(dateTime.Month);
            var year = dateTime.Year;

            return $"{year}-{month}-{day}T00:00:00.000Z";
        }

        private string NumberFormat(int number)
        {
            if (number < 10)
            {
                return $"0{number}";
            }

            return number.ToString();
        }

        private string UrlForDescription(string ebookId)
        {
            //string example = "https://static.packt-cdn.com/products/9781787121706/summary";
            string url = $"https://static.packt-cdn.com/products/{ebookId}/summary";
            return url;
        }
    }
}