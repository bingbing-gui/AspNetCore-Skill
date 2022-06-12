using AspNetCore.Cookie.Data;
using AspNetCore.Cookie.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace AspNetCore.Cookie.Page.Account
{
    public class LoginModel : PageModel
    {

        private readonly ILogger<LoginModel> _logger;

        public LoginModel(ILogger<LoginModel> logger)
        {
            _logger = logger;
        }
        public string? ReturnURL { get; set; }

        [BindProperty]
        public InputModel? Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string? Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string? Password { get; set; }
        }
        public async Task<IActionResult> OnPostAsync(string returnURL = null)
        {
            ReturnURL = returnURL;
            if (ModelState.IsValid)
            {
                var user = await AuthenticateUser(Input?.Email, Input?.Password);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.Email),
                    new Claim("fullname",user.FullName),
                    new Claim(ClaimTypes.Role,"Administrator")
                };
                var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var claimsPrincipal = new ClaimsPrincipal(claimIdentity);

                var authenticationProperties = new AuthenticationProperties()
                { };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    authenticationProperties
                    );
                _logger.LogInformation("User {Email} logged in at {Time}.", user.Email, DateTime.UtcNow);
                return LocalRedirect(Url.GetLocalUrl(ReturnURL));
            }
            return Page();
        }
        public async Task<ApplicationUser> AuthenticateUser(string email, string password)
        {
            await Task.Delay(500);

            if (email == "maria.rodriguez@contoso.com")
            {
                return new ApplicationUser()
                {
                    Email = "maria.rodriguez@contoso.com",
                    FullName = "Maria Rodriguez"
                };
            }
            else
            {
                return null;
            }
        }

    }
}
