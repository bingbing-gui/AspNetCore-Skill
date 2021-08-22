using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AspNetCore.JWT.Practice.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace AspNetCore.JWT
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private List<User> users = new List<User>
        {
        new User { FullName = "zhangsan", UserName = "admin", Password = "1234", UserRole = "Admin"},
        new User { FullName = "wangwu", UserName = "user", Password = "1234", UserRole = "User" }
        };

        private readonly ILogger<LoginController> _logger;

        public LoginController(IConfiguration config, ILogger<LoginController> logger)
        {
            _logger = logger;
            _config = config;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] User loginuser)
        {
            IActionResult response = Unauthorized();
            User user = users.SingleOrDefault(
                            x => x.UserName == loginuser.UserName
                            && x.Password == loginuser.Password);
            if (user != null)
            {
                var tokenString = GenerateJWTToken(user);
                response = Ok(new
                {
                    token = tokenString,
                    userDetails = user,
                });
            }
            return response;
        }
        string GenerateJWTToken(User userInfo)
        {
            //1.创建JwtSecurityTokenHandler
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            //2.创建Private Key 
            var tokenKey = Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]);
            //3.创建SymmetricSecurityKey
            var securityKey = new SymmetricSecurityKey(tokenKey);
            //4.创建SigningCredentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,userInfo.UserName),
                new Claim("fullName",userInfo.FullName.ToString()),
                new Claim("role",userInfo.UserRole),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
            };
            //5.创建JwtSecurityToken
            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}
