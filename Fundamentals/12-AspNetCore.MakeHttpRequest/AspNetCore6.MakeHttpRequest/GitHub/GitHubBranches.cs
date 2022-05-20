using System.Text.Json.Serialization;

namespace AspNetCore6.MakeHttpRequest.GitHub
{
    public record GitHubBranch(
     [property: JsonPropertyName("name")] string Name);
}
