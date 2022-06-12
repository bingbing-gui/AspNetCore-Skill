using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.Cookie.Page
{
    [Authorize(Roles = "Administrator2")]
    public class PrivacyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
