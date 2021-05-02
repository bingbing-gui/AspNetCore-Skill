using AspNetCore.UsingHttpClient.Practice.GitHub;
using AspNetCore.UsingHttpClient.Practice.Models;
using AspNetCore.UsingHttpClient.Practice.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace AspNetCore.UsingHttpClient.Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly GitHubService _gitHubService;
        private readonly RepoService _repoService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger,
            IHttpClientFactory httpClientFactory,
            GitHubService gitHubService,
            RepoService repoService)
        {
            _logger = logger;
            _clientFactory = httpClientFactory;
            _gitHubService = gitHubService;
            _repoService = repoService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> BasicUsage()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "https://api.github.com/repos/dotnet/AspNetCore.Docs/branches");
            request.Headers.Add("Accept", "application/vnd.github.v3+json");
            request.Headers.Add("User-Agent", "HttpClientFactory-Sample");
            var client = _clientFactory.CreateClient();
            var reponse = await client.SendAsync(request);
            BasicUsageModel basicUsageModel = new BasicUsageModel();
            if (reponse.IsSuccessStatusCode)
            {
                using var responseStream = await reponse.Content.ReadAsStreamAsync();
                var Branches = await JsonSerializer.DeserializeAsync
                    <IEnumerable<GitHubBranch>>(responseStream);
                basicUsageModel.Branches = Branches;
                basicUsageModel.GetBranchesError = false;
            }
            else
            {
                var Branches = Array.Empty<GitHubBranch>();
                basicUsageModel.Branches = Branches;
                basicUsageModel.GetBranchesError = true;
            }
            return View(basicUsageModel);
        }

        public async Task<IActionResult> NamedClient()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
                "repos/dotnet/AspNetCore.Docs/pulls");
            var client = _clientFactory.CreateClient("github");
            var response = await client.SendAsync(request);
            NamedClientModel namedClientModel = new NamedClientModel();
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var PullRequests = await JsonSerializer.DeserializeAsync
                         <IEnumerable<GitHubPullRequest>>(responseStream);
                namedClientModel.GetPullRequestsError = false;
                namedClientModel.PullRequests = PullRequests;
            }
            else
            {
                namedClientModel.GetPullRequestsError = true;
                namedClientModel.PullRequests = Array.Empty<GitHubPullRequest>();
            }
            return View(namedClientModel);
        }

        public async Task<IActionResult> TypedClient()
        {
            TypeClientModel typeClientModel = new TypeClientModel();
            try
            {
                var gitHubIssues = await _gitHubService.GetAspNetDocsIssues();
                typeClientModel.LatestIssues = gitHubIssues;
            }
            catch (HttpRequestException ex)
            {
                typeClientModel.GetIssuesError = true;
                typeClientModel.LatestIssues = Array.Empty<GitHubIssue>();
            }
            return View(typeClientModel);
        }
        public async Task<IActionResult> TypedClient2()
        {
            GitHubRepo gitHubRepo = new GitHubRepo();
            try
            {
                var repos = await _repoService.GetRepos();
                gitHubRepo.GetRepoRequestsError = false;
                gitHubRepo.RepoRequests = repos;
            }
            catch (HttpRequestException ex)
            {
                gitHubRepo.GetRepoRequestsError = true;
                gitHubRepo.RepoRequests = Array.Empty<string>();
            }
            return View(gitHubRepo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
