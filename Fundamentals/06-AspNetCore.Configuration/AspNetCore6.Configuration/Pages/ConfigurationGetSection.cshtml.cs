using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace AspNetCore6.Configuration.Pages
{
    public class ConfigurationGetSectionModel : PageModel
    {

        private readonly IConfiguration _rootConfiguration;

        private readonly IConfiguration _configuration;

        private readonly IConfiguration _configuration2;

        public ConfigurationGetSectionModel(IConfiguration configuration)
        {
            _rootConfiguration = configuration;
            _configuration = configuration.GetSection("section1");
            _configuration2 = configuration.GetSection("section2:subsection0");

        }
        public ContentResult OnGet()
        {

            var selection=_rootConfiguration.GetSection("section2");
            if (!selection.Exists())
            {
                throw new Exception("section2 does not exist.");
            }
            var children=selection?.GetChildren();

            var stringBuilder = new StringBuilder();
            foreach (var section in children)
            {
                int i = 0;
                var key1 = section.Key + ":key" + i++.ToString();
                var key2 = section.Key + ":key" + i.ToString();
                stringBuilder.Append(key1 + " value: " + selection[key1] + "\n");
                stringBuilder.Append(key2 + " value: " + selection[key2] + "\n");
            }
            return Content(
                $"section1:key0:{_configuration["key0"]}\n" +
                $"section1:key1:{_configuration["key1"]}\n" +
                $"section2:subsection0:{_configuration2["key0"]}\n" +
                $"section2:subsection0:{_configuration2["key1"]}\n" +
                $"section2:\n{stringBuilder.ToString()}");
        }
    }
}
