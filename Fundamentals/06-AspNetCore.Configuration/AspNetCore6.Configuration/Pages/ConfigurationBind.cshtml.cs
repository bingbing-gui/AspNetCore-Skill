using AspNetCore6.Configuration.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore6.Configuration.Pages
{
    public class ConfigurationBindModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public ConfigurationBindModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ContentResult OnGet()
        {
            var positionOptions = new PositionOptions();
            _configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            return Content("Name: " + positionOptions.Name +"\r" + "Title: " + positionOptions.Title);
        }
    }
}
