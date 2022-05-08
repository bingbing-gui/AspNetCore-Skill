using AspNetCore6.OptionsValidation.Configuration;
using AspNetCore6.OptionsValidation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace AspNetCore6.OptionsValidation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IOptions<MyConfigOptions> _config;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IOptions<MyConfigOptions> config, ILogger<HomeController> logger)
        {
            _config = config;
            _logger = logger;
            try
            {
                var configValue = _config.Value;
            }
            catch (OptionsValidationException ex)
            { 
                foreach(var failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }
            }
        }

        public IActionResult Index()
        {
            string msg;
            try
            {
                msg = $"Key1: {_config.Value.Key1} \n" +
                      $"Key2: {_config.Value.Key2} \n" +
                      $"Key3: {_config.Value.Key3}";
            }
            catch (OptionsValidationException optValEx)
            {
                return Content(optValEx.Message);
            }
            return Content(msg);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}