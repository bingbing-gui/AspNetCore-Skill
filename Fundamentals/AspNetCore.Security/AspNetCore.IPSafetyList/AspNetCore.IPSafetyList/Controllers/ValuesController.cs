using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.IPSafetyList.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            _logger = logger;
        }

        //#region snippet_ActionFilter
        //[ServiceFilter(typeof(ClientIpCheckActionFilter))]
        //[HttpGet]
        //public IEnumerable<string> Get()
        //#endregion snippet_ActionFilter
        //{
        //    _logger.LogDebug("successful HTTP GET");

        //    return new string[] { "value1", "value2" };
        //}

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public void Post(string value)
        {
        }
    }
}