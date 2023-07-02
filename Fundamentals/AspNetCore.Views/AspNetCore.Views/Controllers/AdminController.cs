using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Views.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Users(int id)
        {
            ViewBag.Id = id;
            return View();
        }
    }
}
