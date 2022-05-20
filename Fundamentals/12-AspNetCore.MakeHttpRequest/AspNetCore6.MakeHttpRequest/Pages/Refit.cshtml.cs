using AspNetCore6.MakeHttpRequest.GitHub;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore6.MakeHttpRequest.Pages
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
