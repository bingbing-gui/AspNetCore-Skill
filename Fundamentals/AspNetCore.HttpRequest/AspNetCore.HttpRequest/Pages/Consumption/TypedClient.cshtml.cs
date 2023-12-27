using AspNetCore.HttpRequest.GitHub;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.HttpRequest.Pages.Consumption
{
    public class TypedClientModel : PageModel
    {
        private readonly GitHubService _gitHubService;

        public IEnumerable<GitHubBranch>? GitHubBranches
        {
            get;
            set;
        }
        public TypedClientModel(GitHubService gitHubService)
        {
            _gitHubService = gitHubService;
        }
        public async Task OnGet()
        {
            GitHubBranches = await _gitHubService.GetAspNetCoreDocsBranchesAsync();
        }
    }
}
