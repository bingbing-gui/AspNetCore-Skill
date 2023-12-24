using Refit;

namespace AspNetCore.HttpRequest.GitHub
{
    public interface IGitHubClient
    {
        [Get("/repos/dotnet/AspNetCore.Docs/branches")]
        Task<IEnumerable<GitHubBranch>> GetAspNetCoreDocsBranchesAsync();
    }
}
