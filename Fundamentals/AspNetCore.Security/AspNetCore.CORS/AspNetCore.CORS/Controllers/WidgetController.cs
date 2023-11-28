using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.CORS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WidgetController : ControllerBase
    {
        [EnableCors("AnotherPolicy")]
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "green widget", "red widget" };
        }
        [EnableCors("Policy1")]
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return id switch
            {
                1=>"green widget",
                2=>"red widget",
                _=>NotFound()
            };
        }
    }
}
