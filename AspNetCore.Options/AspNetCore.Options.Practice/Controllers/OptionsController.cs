using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        public OptionsController(IConfiguration configuration, ILogger<OptionsController> logger)
        {
            Configuration = configuration;
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<PositionOptions> Get()
        {
            var positionOptions = new PositionOptions();
            //Configuration.GetSection(PositionOptions.Position).Bind(positionOptions);
            positionOptions = Configuration.GetSection(PositionOptions.Position).Get<PositionOptions>();
            var list = new List<PositionOptions>();
            list.Add(positionOptions);
            return list;
        }
    }
}
