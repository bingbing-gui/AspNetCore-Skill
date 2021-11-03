using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebAppConfiguration.Models;

namespace WebAppConfiguration.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        // requires using Microsoft.Extensions.Configuration;
        private readonly IConfiguration Configuration;

        private readonly PositionOptions _options;
        /// <summary>
        /// 使用IOptions
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        /// <param name="options"></param>
        public HomeController(ILogger<HomeController> logger, IConfiguration configuration, IOptions<PositionOptions> options)
        {
            Configuration = configuration;
            _logger = logger;
            _options = options.Value;
        }

        public IActionResult Index()
        {
            var myKeyValue = Configuration["MyKey"];
            var title = Configuration["Position:Title"];
            var name = Configuration["Position:Name"];
            var defaultLogLevel = Configuration["Logging:LogLevel:Default"];

            return Content($"MyKey value: {myKeyValue} \n" +
                           $"Title: {title} \n" +
                           $"Name: {name} \n" +
                           $"Default Log Level: {defaultLogLevel}");
        }
        /// <summary>
        /// 1.ConfigurationBinder.Bind
        /// 2.ConfigurationBinder.Get<T> 
        /// </summary>
        /// <returns></returns>
        public IActionResult Privacy()
        {
            var positionOptions = new PositionOptions();
            Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);

            positionOptions = Configuration.GetSection(PositionOptions.Position)
                                                   .Get<PositionOptions>();
            return Content($"Title: {positionOptions.Title} \n" +
                          $"Name: {positionOptions.Name}");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
