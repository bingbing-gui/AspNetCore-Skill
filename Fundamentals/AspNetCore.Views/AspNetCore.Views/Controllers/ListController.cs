using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Views.Controllers
{
    public class ListController : Controller
    {
        public IActionResult Search()
        {
            return View();
        }

    }
}
