using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore6.Configuration.Pages
{
    public class TestModel : PageModel
    {
        private readonly IConfiguration _configuration;

        public TestModel(IConfiguration configuration)
        { 
            _configuration = configuration;
        }
        public ContentResult OnGet()
        {
            var myKeyValue = _configuration["MyKey"];
            var title = _configuration["Position:Title"];
            var name = _configuration["Position:Name"];
            var defaultLogLevel = _configuration["Logging:LogLevel:Default"];
            return Content($"MyKey value: {myKeyValue} \n" +
                           $"Title: {title} \n" +
                           $"Name: {name} \n" +
                           $"Default Log Level: {defaultLogLevel}");
        }
    }
}
