using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AspNetCore.API.BasicAuthentication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasicAuthController : ControllerBase
    {
        public BasicAuthController()
        {

        }
        [Authorize]
        [HttpGet("auth", Name = "BasicAuth")]
        public ActionResult BasicAuth()
        {
            var response = new
            {
                Message = "授权成功",
                ServerTime = DateTime.Now,
            };
            return Ok(response);
        }
    }
}