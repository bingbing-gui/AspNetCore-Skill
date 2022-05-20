using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace AspNetCore6.MakeHttpRequest.HttpVerb
{
    public class TodoClient
    {
        private readonly HttpClient _httpClient;

        public TodoClient(HttpClient httpClient) =>
            _httpClient = httpClient;

        public async Task CreateItemAsync(TodoItem todoItem)
        {
            var todoItemJson = new StringContent(
                JsonSerializer.Serialize(todoItem),
                Encoding.UTF8,
                Application.Json);

            using var httpResponseMessage = await _httpClient.PostAsync("/api/TodoItems", todoItemJson);

            httpResponseMessage.EnsureSuccessStatusCode();
        }
        public async Task SaveItemAsync(TodoItem todoItem)
        {
            var todoItemJson = new StringContent(
                JsonSerializer.Serialize(todoItem),
                Encoding.UTF8,
                Application.Json
                );
            using var httpResponseMessage = await _httpClient.PutAsync($"/api/TodoItems/{todoItem.Id}", todoItemJson);
            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task DeleteItemAsync(TodoItem todoItem)
        {
            var todoItemJson = new StringContent(JsonSerializer.Serialize(todoItem), Encoding.UTF8, Application.Json);

            using var httpResponseMessage = await _httpClient.DeleteAsync($"/api/TodoItems/{todoItem.Id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }
    }

    public record TodoItem(
        [property: JsonPropertyName("id")] long Id,
        [property: JsonPropertyName("name")] string Name,
        [property: JsonPropertyName("isComplete")] bool IsComplete);
}
