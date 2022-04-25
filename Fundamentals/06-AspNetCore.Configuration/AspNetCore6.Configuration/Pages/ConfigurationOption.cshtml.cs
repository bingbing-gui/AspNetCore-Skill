using AspNetCore6.Configuration.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AspNetCore6.Configuration.Pages
{
    public class ConfigurationOption : PageModel
    {
        private readonly PositionOptions _positionOptions;
        public ConfigurationOption(IOptions<PositionOptions> options)
        {
            _positionOptions = options.Value;
        }
        public ContentResult OnGet()
        {
            return Content("Name:" + _positionOptions.Name + "\r" + 
                "Title:" + _positionOptions.Title);
        }
    }
}
