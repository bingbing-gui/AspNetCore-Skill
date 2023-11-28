using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Docs.Samples;

namespace AspNetCore.CORS.Controllers
{
    /*
    没有通过终结点路由启用 CORS。
    没有定义默认 CORS 策略。
    使用 [EnableCors("MyPolicy")] 为控制器启用 "MyPolicy" CORS 策略。
    为 GetValues2 方法禁用 CORS。
     
     */
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public IActionResult Get() =>
            ControllerContext.MyDisplayRouteInfo();

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id) =>
            ControllerContext.MyDisplayRouteInfo(id);

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id) =>
            ControllerContext.MyDisplayRouteInfo(id);


        // GET: api/values/GetValues2
        [DisableCors]
        [HttpGet("{action}")]
        public IActionResult GetValues2() =>
            ControllerContext.MyDisplayRouteInfo();

    }
}
