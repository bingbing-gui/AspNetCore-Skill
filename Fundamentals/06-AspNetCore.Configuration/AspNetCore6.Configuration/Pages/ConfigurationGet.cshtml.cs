using AspNetCore6.Configuration.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore6.Configuration.Pages
{
    public class ConfigurationGetModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public PositionOptions? PositionOptions { get; set; }

        public ConfigurationGetModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public ContentResult OnGet()
        {
            PositionOptions = _configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();

            return Content("Name :"+PositionOptions.Name+"\r" +"Title :"+ PositionOptions.Title);
        }
    }
}
