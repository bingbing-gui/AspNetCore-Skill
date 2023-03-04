using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Identity.Controllers
{
    public class ClaimsController : Controller
    {
        private UserManager<AppUser> _userManager;
        public ClaimsController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(User.Claims);
        }
        public IActionResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(string claimType, string claimValue)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            Claim claim = new Claim(claimType, claimValue, ClaimValueTypes.String);
            IdentityResult result = await _userManager.AddClaimAsync(user ?? new AppUser(), claim);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string claimValues)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            string[] claimValuesArray = claimValues.Split(";");
            string claimType = claimValuesArray[0], claimValue = claimValuesArray[1], claimIssuer = claimValuesArray[2];
            Claim? claim = User.Claims.Where(x => x.Type == claimType && x.Value == claimValue && x.Issuer == claimIssuer).FirstOrDefault();
            IdentityResult result = await _userManager.RemoveClaimAsync(user ?? new AppUser(), claim);
            if (result.Succeeded)
                return RedirectToAction("Index");
            else
                Errors(result);
            return View("Index");
        }
        void Errors(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
                ModelState.AddModelError("", error.Description);
        }
    }
}
