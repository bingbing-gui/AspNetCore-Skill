using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Views.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CallSharedView()
        {
            return View();
        }
    }
}
