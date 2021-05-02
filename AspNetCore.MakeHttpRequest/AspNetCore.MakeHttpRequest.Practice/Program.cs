using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetCore.MakeHttpRequest.Practice
{
    class Program
    {
        static async Task<int> Main(string[] args)
        {
            var builder = new HostBuilder()
                .ConfigureServices((hostContext,services)=> 
                {
                    services.AddHttpClient();
                    services.AddTransient<IMyService, MyService>();
                })
                .UseConsoleLifetime();
            var host = builder.Build();
            try
            {
                var myService=host.Services.GetRequiredService<IMyService>();
                var pageContent = await myService.GetPage();
                Console.WriteLine(pageContent);
            }
            catch (Exception ex)
            {
                var logger = host.Services.GetRequiredService<ILogger<Program>>();

                logger.LogError(ex, "An error occurred.");
            }
            return 0;
        }
    }
    public interface IMyService
    {
        Task<string> GetPage();
    }
    public class MyService : IMyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public MyService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<string> GetPage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://www.bbc.co.uk/programmes/b006q2x0");

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
            else
            {
                return $"StatusCode: {response.StatusCode}";
            }
        }
    }
}
