using Microsoft.AspNetCore.Mvc;
using PartialViews.Models;
using System.Diagnostics;

namespace PartialViews.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Discovery() => View();

    }
}