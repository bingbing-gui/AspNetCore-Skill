using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Options.Bind.Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionsBindController : ControllerBase
    {
        private readonly ILogger<OptionsBindController> _logger;

        private readonly IConfiguration Configuration;

        private readonly PositionOptions _options;

        private readonly TopItemSettings _monthTopItem;

        private readonly TopItemSettings _yearTopItem;

        public OptionsBindController(IConfiguration configuration,
            IOptionsMonitor<PositionOptions> options,
            IOptionsSnapshot<TopItemSettings> optionsSnapshot,
            ILogger<OptionsBindController> logger
            )
        {
            Configuration = configuration;
            _options = options.CurrentValue;
            _monthTopItem = optionsSnapshot.Get(TopItemSettings.Month);
            _yearTopItem = optionsSnapshot.Get(TopItemSettings.Year);
            _logger = logger;
            //_config = config;
            //try
            //{
            //    var configValue = _config.Value;
            //}
            //catch (OptionsValidationException ex)
            //{
            //    foreach (var failure in ex.Failures)
            //    {
            //        _logger.LogError(failure);
            //    }
            //}
        }
        /// <summary>
        ///通过Bind方法将配置文件绑定到实体类
        ///Configuration.GetSection(PositionOptions.Position).Bind(positionOptions); 
        /// </summary>
        /// <returns></returns>
        [HttpGet("bind")]
        public int BindConfigeToEntityByBindMethod()
        {
            //第一种方法
            var positionOptions = new PositionOptions();
            Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            Console.WriteLine($"Positioin Bind");
            Console.WriteLine($"PositionOptions.Title= {positionOptions.Title}");
            Console.WriteLine($"PositionOptions.Name= {positionOptions.Name}");
            return 1;
        }
        /// <summary>
        /// 通过get 方法将配置文件绑定到一个实体类
        ///positionOptions = Configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();
        /// </summary>
        /// <returns></returns>
        [HttpGet("get")]
        public int BindConfigeToEntityByGetMethod()
        {
            //第二种方法
            var positionOptions = new PositionOptions();
            positionOptions = Configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();
            Console.WriteLine($"Positioin Get");
            Console.WriteLine($"PositionOptions.Title= {positionOptions.Title}");
            Console.WriteLine($"PositionOptions.Name= {positionOptions.Name}");
            return 1;
        }
        /// <summary>
        /// 获取month 实例
        /// </summary>
        /// <returns></returns>
        [HttpGet("monthsection")]
        public int GetMonthSection()
        {
            Console.WriteLine($"Month Get");
            Console.WriteLine($"Month section= {_monthTopItem.Name}");
            Console.WriteLine($"Month section= {_monthTopItem.Model}");
            return 1;
        }
        /// <summary>
        /// 获取year 实例
        /// </summary>
        /// <returns></returns>
        [HttpGet("yearsection")]
        public int GetYearSection()
        {
            Console.WriteLine($"Month Get");
            Console.WriteLine($"Month setcion= {_yearTopItem.Name}");
            Console.WriteLine($"Month setcion= {_yearTopItem.Model}");
            return 1;
        }
        //[HttpGet("get2")]
        //public ContentResult Get2()
        //{
        //    string msg;
        //    try
        //    {
        //        msg = $"Key1: {_config.Value.Key1} \n" +
        //              $"Key2: {_config.Value.Key2} \n" +
        //              $"Key3: {_config.Value.Key3}";
        //    }
        //    catch (OptionsValidationException optValEx)
        //    {
        //        return Content(optValEx.Message);
        //    }
        //    return Content(msg);
        //}
    }
}
