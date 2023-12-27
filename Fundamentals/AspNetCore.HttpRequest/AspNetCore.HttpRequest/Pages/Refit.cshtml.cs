using AspNetCore.HttpRequest.GitHub;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace AspNetCore.HttpRequest.Pages
{
    public class RefitModel : PageModel
    {
        private readonly IGitHubClient _gitHubClient;

        public IEnumerable<GitHubBranch> GitHubBranches { get; set; }
        public RefitModel(IGitHubClient gitHubClient)
        {
            _gitHubClient = gitHubClient;
        }
        public async Task OnGet()
        {
            GitHubBranches = await _gitHubClient.GetAspNetCoreDocsBranchesAsync();
        }
    }
}
