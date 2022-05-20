using AspNetCore6.MakeHttpRequest.GitHub;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore6.MakeHttpRequest.ViewComponents;
public class GitHubBranchesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IEnumerable<GitHubBranch>? gitHubBranches) =>
        View(gitHubBranches);
}
