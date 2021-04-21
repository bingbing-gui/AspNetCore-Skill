using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Options.Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Options.Practice2.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public int Get([FromServices] IOrderService orderService)
        {
            Console.WriteLine($"The IOptions Patter ShowIOptionsMaxOrderCount:{orderService.ShowIOptionsMaxOrderCount()}");
            //能够读取到最新配置
            Console.WriteLine($"The IOptionsMonitor Patter ShowIOptionsMonitorOrderCount:{orderService.ShowIOptionsMonitorOrderCount()}");
            Console.WriteLine($"The IOptionsSnapshotMax Patter ShowIOptionsSnapshotMaxOrderCount:{orderService.ShowIOptionsSnapshotMaxOrderCount()}");
            return 1;
        }
    }
}
