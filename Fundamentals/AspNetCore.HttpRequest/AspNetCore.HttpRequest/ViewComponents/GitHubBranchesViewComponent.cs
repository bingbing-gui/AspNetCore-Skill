using AspNetCore.HttpRequest.GitHub;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.HttpRequest.ViewComponents;
public class GitHubBranchesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke(IEnumerable<GitHubBranch>? gitHubBranches) =>
        View(gitHubBranches);
}
