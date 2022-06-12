using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.Cookie.Page
{
    public class ForbiddenModel : PageModel
    {
        public string? Message { get; set; }

        public void OnGet()
        {
            Message = "Forbidden page.";
        }
    }
}
