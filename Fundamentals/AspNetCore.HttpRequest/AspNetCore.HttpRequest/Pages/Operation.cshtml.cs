using AspNetCore.HttpRequest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.HttpRequest.Pages
{
    public class OperationModel : PageModel
    {
        private readonly IOperationScoped _operationScoped;

        private readonly IHttpClientFactory _httpClientFactory;

        public OperationModel(IOperationScoped operationScoped, IHttpClientFactory httpClientFactory)
            => (_operationScoped, _httpClientFactory) = (operationScoped, httpClientFactory);

        public string OperationIdFromRequestScoped { get; set; } = string.Empty;
        public string OperationIdFromHandlerScoped { get; set; } = string.Empty;

        public async Task OnGet()
        {
            var httpClient = _httpClientFactory.CreateClient("Operation");
            OperationIdFromHandlerScoped = _operationScoped.OperationId;
            OperationIdFromRequestScoped = await httpClient.GetStringAsync("https://www.baidu.com");
        }
    }
}
