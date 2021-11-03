using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FileConfigurationProvider.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration Configuration;

        public IndexModel(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public ContentResult OnGet()
        {
            var key00 = "section:section0:key:key0";
            var key01 = "section:section0:key:key1";
            var key10 = "section:section1:key:key0";
            var key11 = "section:section1:key:key1";

            var val00 = Configuration[key00];
            var val01 = Configuration[key01];
            var val10 = Configuration[key10];
            var val11 = Configuration[key11];

            return Content($"{key00} value: {val00} \n" +
                           $"{key01} value: {val01} \n" +
                           $"{key10} value: {val10} \n" +
                           $"{key10} value: {val11} \n"
                           );
        }
    }
}
