using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.RazorPage.HttpContext.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Razor 中访问HttpContext
        /// </summary>
        public void OnGet()
        {
            var pathBase = HttpContext.Request.PathBase;
        }
    }
}