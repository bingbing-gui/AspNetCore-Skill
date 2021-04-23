using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Options.Validation.Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        private readonly IOptions<MyConfigOptions> _option;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IOptions<MyConfigOptions> options)
        {
            _logger = logger;
            _option = options;
            _option = options;
            try
            {
                var configValue = _option.Value;
            }
            catch (OptionsValidationException ex)
            {
                foreach (var failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }
            }
        }
        [HttpGet("validation")]
        public int Get()
        {
            try
            {
                Console.WriteLine($"Key1={_option.Value.Key1}");
                Console.WriteLine($"Key1={_option.Value.Key2}");
                Console.WriteLine($"Key1={_option.Value.Key3}");
            }
            catch (OptionsValidationException optValEx)
            {
                _logger.LogError(optValEx.Message);
            }
            return 1;
        }
    }
}
