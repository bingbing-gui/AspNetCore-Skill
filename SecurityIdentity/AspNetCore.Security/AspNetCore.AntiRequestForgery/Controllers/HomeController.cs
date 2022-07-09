using AspNetCore.AntiRequestForgery.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AspNetCore.AntiRequestForgery.Controllers
{
    /*
     ValidateAntiForgeryToken和AutoValidateAntiforgeryToken
     区别: AutoValidateAntiforgeryToken 不验证下列请求 
        GET
        HEAD
        OPTIONS
        TRACE
     */
    [AutoValidateAntiforgeryToken]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [HttpOptions]
        [ValidateAntiForgeryToken]
        public IActionResult Index()
        {
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