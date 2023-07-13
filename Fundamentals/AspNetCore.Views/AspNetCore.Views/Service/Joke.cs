using System.Text.Json;

namespace AspNetCore.Views.Service
{
    public class Joke
    {
        public string type { get; set; }
        public Value value { get; set; }

        public async Task<string> GetJoke()
        {
            string apiResponse = "";
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://api.vvhan.com/api/joke"))
                {
                    apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return apiResponse;
        }
    }

    public class Value
    {
        public int id { get; set; }
        public string joke { get; set; }
        public object[] categories { get; set; }
    }
}
