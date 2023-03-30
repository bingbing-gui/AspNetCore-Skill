using AspNetCore.DependencyInjection.Interfaces;
using AspNetCore.DependencyInjection.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCore.DependencyInjection.Controllers
{
    public class HomeController : Controller
    {

        private readonly IOperationService _operationService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IOperationService operationService, ILogger<HomeController> logger)
        {
            _operationService = operationService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            _operationService.TestLifetime();
            return View();
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