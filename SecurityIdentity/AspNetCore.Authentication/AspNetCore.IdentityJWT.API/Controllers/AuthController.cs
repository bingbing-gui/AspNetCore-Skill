using AspNetCore.IdentityJWT.API.Services;
using AspNetCore.IdentityJWT.API.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.IdentityJWT.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        public AuthController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.RegisterUserAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmailAsync(string userId, string token)
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(token))
                return NotFound();

            var result = await _userService.ConfirmEmailAsync(userId, token);

            if (result.IsSuccess)
            {
                return Redirect($"{_configuration["DomainUrl"]}/ConfirmEmail.html");
            }
            return BadRequest("");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.LoginUserAsync(model);
                if (result.IsSuccess)
                {

                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpGet("ForgetPassword")]
        public async Task<IActionResult> ForgetPasswordAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return NotFound();
            var result = await _userService.ForgetPasswordAsync(email);
            if (result.IsSuccess)
                return Ok(result); // 200
            return BadRequest(result); // 400
        }
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPasswordAsync([FromForm] ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.ResetPasswordAsync(model);
                if (result.IsSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }
    }
}
