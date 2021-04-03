using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Options.Practice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OptionsController : ControllerBase
    {
        private readonly ILogger<OptionsController> _logger;

        private readonly IConfiguration Configuration;

        private readonly PositionOptions _options;

        private readonly TopItemSettings _monthTopItem;

        private readonly TopItemSettings _yearTopItem;

        private readonly IOptions<MyConfigOptions> _config;

        public OptionsController(IConfiguration configuration, IOptionsMonitor<PositionOptions> options,
            IOptionsSnapshot<TopItemSettings> optionsSnapshot,
            IOptions<MyConfigOptions> config,
            ILogger<OptionsController> logger)
        {
            Configuration = configuration;
            _options = options.CurrentValue;
            _monthTopItem = optionsSnapshot.Get(TopItemSettings.Month);
            _yearTopItem = optionsSnapshot.Get(TopItemSettings.Year);
            _logger = logger;
            _config = config;
            try
            {
                var configValue = _config.Value;
            }
            catch (OptionsValidationException ex)
            {
                foreach (var failure in ex.Failures)
                {
                    _logger.LogError(failure);
                }
            }
        }

        [HttpGet]
        public IEnumerable<PositionOptions> Get()
        {
            var positionOptions = new PositionOptions();
            //第一种方法
            //Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            //第二种方法
            //positionOptions = Configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();
            var list = new List<PositionOptions>();
            list.Add(_options);
            return list;
        }
        [HttpGet("get2")]
        public ContentResult Get2()
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
    }
}
