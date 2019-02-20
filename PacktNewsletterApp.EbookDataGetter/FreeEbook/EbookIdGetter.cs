using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PacktNewsletterApp.EbookDataGetter.FreeEbook
{
    public class EbookIdGetter
    {
        public async Task<string> GetId()
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(UrlForId());
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();

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
    }
}