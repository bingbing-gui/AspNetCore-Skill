using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Log.Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        /*
        1. 创建日志,若要创建日志，需要使用依赖项注入 (DI) 中的 ILogger<TCategoryName>对象。 
            1.1 创建一个记录器 ILogger<WeatherForecastController>,该记录器使用类型为 AboutModel 的完全限定名称的日志类别。 
            日志类别是与每个日志关联的字符串。
            1.2 调用 LogInformation 以在 Information 级别登录。 日志“级别”代表所记录事件的严重程度。
        2. 日志类别
           2.1
           创建 ILogger 对象时，将指定类别。该类别包含在由此 ILogger 实例创建的每条日志消息中。 
           类别字符串是任意的，但约定将使用类名称
           例如，在控制器中，名称可能为 "AspNetCore.Log.Practice.Controllers.WeatherForecastController"。 
            ASP.NET Core Web 应用使用 ILogger<T> 自动获取使用完全限定类型名称 T 作为类别的 ILogger 实例
           2.2
           要显式指定类别，请调用 ILoggerFactory.CreateLogger：
           _logger = loggerFactory.CreateLogger("MyCategory");
        */
        //private readonly ILogger<WeatherForecastController> _logger;
        private readonly ILogger _logger;
        public WeatherForecastController(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger("MyCategory");
        }

        public string Message { get; set; }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

            var rng = new Random();
            try
            {
                Message = $"About page visited at {DateTime.UtcNow.ToLongTimeString()}";
                _logger.LogInformation(Message);
                _logger.LogInformation(MyLogEvents.ListItems, "GetListItem");
                var routeInfo = ControllerContext.ToCtxString(Message);
                _logger.LogInformation(routeInfo);
            }
            catch (Exception ex)
            {
               
            }
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<WeatherForecast>> GetTodoItem(long id)
        //{
        //    _logger.LogInformation(MyLogEvents.GetItem, "Getting item {Id}", id);
        //    //if (todoItem == null)
        //    //{
        //    //    _logger.LogWarning(MyLogEvents.GetItemNotFound, "Get({Id}) NOT FOUND", id);
        //    //    return NotFound();
        //    //}

        //    //return ItemToDTO(todoItem);
        //}
    }
}
