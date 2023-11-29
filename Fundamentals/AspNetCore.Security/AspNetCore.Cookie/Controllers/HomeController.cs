using AspNetCore.Cookie.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
<<<<<<< HEAD

=======
>>>>>>> 935a62729cc87e73a0f64826c7000ced97ebce3d
namespace AspNetCore.Cookie.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
<<<<<<< HEAD

=======
>>>>>>> 935a62729cc87e73a0f64826c7000ced97ebce3d
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
<<<<<<< HEAD
=======
        [HttpGet]
>>>>>>> 935a62729cc87e73a0f64826c7000ced97ebce3d
        public IActionResult Login()
        {
            return View();
        }
<<<<<<< HEAD
        [HttpPost]
=======
>>>>>>> 935a62729cc87e73a0f64826c7000ced97ebce3d
        public async Task<IActionResult> Login(string username, string password, string ReturnUrl)
        {
            if ((username == "Admin") && (password == "Admin"))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Secured" : ReturnUrl);
            }
            else
                return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Privacy()
        {
            return View();
        }
<<<<<<< HEAD

=======
>>>>>>> 935a62729cc87e73a0f64826c7000ced97ebce3d
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}