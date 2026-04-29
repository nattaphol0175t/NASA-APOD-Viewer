using Newtonsoft.Json;
using NasaApodWeb.Models;

namespace NasaApodWeb.Services
{
    public class NasaService
    {
        private readonly string apiKey = "uekRcSBDRD08Tg2hVmDlpDL11OFcYfGO6z7MSh0w";

        public async Task<List<Apod>> GetApods(int year, int month)
        {
            var start = new DateTime(year, month, 1);

            var end = start.AddDays(6);

            string url = $"https://api.nasa.gov/planetary/apod?" +
                         $"api_key={apiKey}" +
                         $"&start_date={start:yyyy-MM-dd}" +
                         $"&end_date={end:yyyy-MM-dd}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "NasaApp");

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    var err = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"API ERROR: {response.StatusCode}");
                    Console.WriteLine(err);

                    return new List<Apod>();
                }

                var json = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<List<Apod>>(json)
                           ?? new List<Apod>();

                return data.Where(x => x.media_type == "image").ToList();
            }
        }

        public async Task<Apod?> GetApodByDate(string date)
        {
            string url = $"https://api.nasa.gov/planetary/apod?" +
                         $"api_key={apiKey}" +
                         $"&date={date}";

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "NasaApp");

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"API ERROR: {response.StatusCode}");
                    return null;
                }

                var json = await response.Content.ReadAsStringAsync();

                var item = JsonConvert.DeserializeObject<Apod>(json);

                // 🔥 กัน video
                if (item?.media_type != "image")
                    return null;

                return item;
            }
        }
    }
}