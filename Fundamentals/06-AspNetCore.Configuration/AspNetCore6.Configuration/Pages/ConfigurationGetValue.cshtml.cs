using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore6.Configuration.Pages
{
    public class ConfigurationGetValueModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public ConfigurationGetValueModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ContentResult OnGet()
        {
            int numberKey=_configuration.GetValue<int>("NumberKey", 99);
            return Content(numberKey.ToString());
        }
    }
}
