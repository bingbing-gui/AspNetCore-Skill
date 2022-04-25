using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace AspNetCore6.Configuration.Pages
{
    public class ConfigurationProvidersModel : PageModel
    {
        private readonly IConfigurationRoot _configurationRoot;
        public ConfigurationProvidersModel(IConfiguration configRoot)
        {
            _configurationRoot = (IConfigurationRoot)configRoot;
        }
        public ContentResult OnGet()
        {
            var stringBuilder = new StringBuilder();
            foreach (var configurationProvider in _configurationRoot.Providers)
            {
                stringBuilder.Append(configurationProvider.ToString()+"\n");
            }
            return Content(stringBuilder.ToString());
        }
    }
}
