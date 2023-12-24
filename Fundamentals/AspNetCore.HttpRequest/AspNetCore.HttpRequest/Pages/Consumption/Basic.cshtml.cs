using AspNetCore.HttpRequest.GitHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace AspNetCore.HttpRequest.Pages.Consumption
{
    public class BasicModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public IEnumerable<GitHubBranch>? GitHubBranches { get; set; }

        public BasicModel(IHttpClientFactory httpClientFactory) =>
       _httpClientFactory = httpClientFactory;

        public async Task OnGet()
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches")
            {
                Headers =
                {
                    {HeaderNames.Accept, "application/vnd.github.v3+json"},
                    {HeaderNames.UserAgent,"HttpRequestsSample"},
                },
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();
                GitHubBranches = await JsonSerializer.DeserializeAsync<IEnumerable<GitHubBranch>>(contentStream);
            }
        }
    }
}
