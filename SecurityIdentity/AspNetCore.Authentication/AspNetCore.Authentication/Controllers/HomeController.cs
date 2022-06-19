using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetCore.Authentication.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize]
        public IActionResult Secret()
        {
            return View();
        }
        [Authorize(Policy = "Claim.DoB")]
        public IActionResult SecretPolicy()
        {
            return View();
        }
        public IActionResult Authenticate()
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob"),
                new Claim(ClaimTypes.Email,"Bob@fmail.com"),
                new Claim("Grandma.Says","Very nice boi"),
                new Claim(ClaimTypes.Role,"Admin"),
                new Claim(ClaimTypes.DateOfBirth,"2020/11/11")
            };
            var licenseClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,"Bob K Foo"),
                new Claim("DrivingLicense","A+"),
            };
            var claimsIdentity = new ClaimsIdentity(claims, "Grandma identity");

            var licenseIdentity = new ClaimsIdentity(licenseClaims, "Government");

            var claimsPrincipal = new ClaimsPrincipal(new[] { claimsIdentity, licenseIdentity });

            HttpContext.SignInAsync(claimsPrincipal);
            return RedirectToAction("Index");
        }
    }
}
