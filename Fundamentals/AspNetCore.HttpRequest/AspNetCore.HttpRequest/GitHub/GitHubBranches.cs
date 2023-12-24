using System.Text.Json.Serialization;

namespace AspNetCore.HttpRequest.GitHub
{
    public record GitHubBranch(
     [property: JsonPropertyName("name")] string Name);
}
