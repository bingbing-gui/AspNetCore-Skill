using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.OptionsPattern.Controllers
{
    public class IOptionsInterfaceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
