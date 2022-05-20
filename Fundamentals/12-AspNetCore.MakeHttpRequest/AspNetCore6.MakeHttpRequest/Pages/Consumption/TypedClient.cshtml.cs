using AspNetCore6.MakeHttpRequest.GitHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore6.MakeHttpRequest.Pages.Consumption
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
