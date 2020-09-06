using AspNetCore.JWT.Practice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.JWT.Practice.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet("user")]
        [Authorize(Policy = Policies.User)]
        public IActionResult GetUser()
        {
            return Ok("这是个普通用户");
        }
        [HttpGet("admin")]
        [Authorize(Policy = Policies.Admin)]
        public IActionResult GetAdmin()
        {
            return Ok("这是一个Admin用户");
        }
    }
}
