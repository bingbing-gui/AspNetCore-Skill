using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Cookie.Controllers
{
    [Authorize]
    public class SecuredController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
<<<<<<< HEAD
}
=======
}
>>>>>>> 935a62729cc87e73a0f64826c7000ced97ebce3d
