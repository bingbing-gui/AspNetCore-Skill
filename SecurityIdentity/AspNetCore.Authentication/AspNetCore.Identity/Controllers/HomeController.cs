using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace AspNetCore.Identity.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailService _emailService;
        public HomeController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Secret()
        {
            return View();
        }
        public IActionResult Fail(string errorMessage)
        {
            ViewData["errorMessage"] = errorMessage;
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var identityUser = await _userManager.FindByNameAsync(username);
            if (identityUser != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(identityUser, password, false, false);
                if (signInResult.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return RedirectToAction(nameof(Fail), new { ErrorMessage = "Login Failure" });
                }
            }
            return RedirectToAction(nameof(Fail), new { ErrorMessage = "Illegal User" });
        }
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(string username, string password)
        {
            IdentityUser identityUser = new IdentityUser()
            {
                UserName = username,
                Email = ""
            };

            var identityResult = await _userManager.CreateAsync(identityUser, password);
            if (identityResult.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);

                string link = Url.Action(nameof(VerifyEmail), "Home", new { userId = identityUser.Id, code = token }, Request!.Scheme, Request.Host!.ToString());
                //await _emailService.SendAsync("test@test.com", "email verify", $"<a href=\"{link}\">Verify Email</a>", true);
                //return RedirectToAction(nameof(EmailVerification),"Home",link);
                return Redirect(link);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ReCreateToken()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ReCreateToken(string userId)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser != null)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                ViewData["token"] = token;
            }
            return View();
        }

        public async Task<IActionResult> VerifyEmail(string userId, string code)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser != null)
            {
                var identityResult = await _userManager.ConfirmEmailAsync(identityUser, code);
                if (identityResult.Succeeded)
                {
                    return View();
                }
                else
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }
        public IActionResult EmailVerification()
        {
            return View();
        }
    }
}
