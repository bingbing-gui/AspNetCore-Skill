using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PartialViews.Pages
{
    public class DiscoveryModel : PageModel
    {
        public void OnGet() => Page();

        #region snippet_OnGetPartial
        public IActionResult OnGetPartial() =>
            Partial("_AuthorPartialRP");
        #endregion
    }
}
