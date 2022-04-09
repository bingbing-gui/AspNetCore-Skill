using AspNetCore.Route.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Route.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly LinkGenerator _linkGenerator;
        public HomeController(ILogger<HomeController> logger,LinkGenerator linkGenerator)
        {
            _logger = logger;
            _linkGenerator = linkGenerator;
        }

        public IActionResult Index()
        {
            var url = _linkGenerator.GetPathByAction("ListProducts", "Store");
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
