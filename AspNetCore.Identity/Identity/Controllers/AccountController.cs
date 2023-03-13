using Identity.CommonService;
using Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;

namespace Identity.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private CommonService.IEmailService _emailService;
        public AccountController(UserManager<AppUser> userManager,
                                 SignInManager<AppUser> signInManager,
                                 CommonService.IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            var login = new Login();
            login.ReturnUrl = returnUrl;
            return View(login);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var appUser = await _userManager.FindByEmailAsync(login.Email);
                if (appUser != null)
                {
                    await _signInManager.SignOutAsync();
                    var signInResult = await _signInManager.PasswordSignInAsync(appUser, login.Password,
                        login.RememberMe, false);
                    if (signInResult.Succeeded)
                    {
                        return Redirect(login.ReturnUrl ?? "/");
                    }
                    var emailStatus = await _userManager.IsEmailConfirmedAsync(appUser);
                    if (emailStatus == false)
                    {
                        ModelState.AddModelError(nameof(login.Email), "Email为确认，请首先确认!");
                    }
                    #region 启用2FA登录
                    //if (appUser.TwoFactorEnabled)
                    //{
                    //    return RedirectToAction("LoginTwoStep", new { Email = appUser.Email, ReturnUrl = login.ReturnUrl });
                    //}
                    #endregion
                }
                ModelState.AddModelError(nameof(login.Email), "Login Failed: Invalid Email or password");
            }
            return View(login);
        }

        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(string email, string returnUrl)
        {
            var appUser = await _userManager.FindByEmailAsync(email);
            //创建Token
            var token = await _userManager.GenerateTwoFactorTokenAsync(appUser ?? new AppUser(), "Email");
            //发送邮件
            _emailService.Send(appUser?.Email ?? "bingbing.gui@outlook.com", "授权码", $"<h2>{token}</h2>");
            //发送SMS
            //_smsService.Send(appUser?.PhoneNumber ?? "13333333333", token);
            return View("LoginTwoStep", new TwoFactor { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginTwoStep(TwoFactor twoFactor, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View("LoginTwoStep", new TwoFactor { TwoFactorCode = twoFactor.TwoFactorCode, ReturnUrl = returnUrl });
            }
            var result = await _signInManager.TwoFactorSignInAsync("Email", twoFactor.TwoFactorCode, false, false);
            if (result.Succeeded)
            {
                return Redirect(returnUrl ?? "/");
            }
            else
            {
                ModelState.AddModelError("", "登录失败");
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
