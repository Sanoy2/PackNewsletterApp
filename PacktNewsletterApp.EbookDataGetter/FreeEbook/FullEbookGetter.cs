using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PacktNewsletterApp.EbookDataGetter.FreeEbook
{
    public class FullEbookGetter
    {
        public async Task<string> GetFullEbookAsJSON()
        {
            var idGetter = new EbookIdGetter();
            var ebookId = await idGetter.GetId();
            var urlForDescription = UrlForDescription(ebookId);
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(urlForDescription);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

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

        private string UrlForDescription(string ebookId)
        {
            string url = $"https://static.packt-cdn.com/products/{ebookId}/summary";
            return url;
        }
    }
}