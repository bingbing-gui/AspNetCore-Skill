using AspNetCore.ModelBinding.Advanced.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
namespace AspNetCore.ModelBinding.Advanced.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
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
        //public IActionResult Places(string[] places) => View(places);

        public IActionResult Places(List<string> places) => View(places);

        public IActionResult Address() => View();

        [HttpPost]
        public IActionResult Address(List<PersonAddress> address) => View(address);


        public IActionResult FromFormExample() => View();

        [HttpPost]
        public IActionResult FromFormExample([FromForm] Employee model)
        {
            ViewBag.Message = "Employee data received";
            return View();
        }
    }
}