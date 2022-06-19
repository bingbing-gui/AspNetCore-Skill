using AspNetCore.IdentityJWT.API.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace AspNetCore.IdentityJWT.API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IConfiguration _configuration;
        public UserService(UserManager<IdentityUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            if (model == null)
                throw new ArgumentNullException("Register Model is null");
            if (model.Password != model.ConfirmPassword)
                return new UserManagerResponse
                {
                    Message = "Confirm password doesn't match the password",
                    IsSuccess = false
                };
            var identityUser = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var identityResult = await _userManager.CreateAsync(identityUser, model.Password);
            if (identityResult.Succeeded)
            {
                #region Email Verify

                #endregion
                return new UserManagerResponse
                {
                    Message = "User created sucessfully!",
                    IsSuccess = false
                };
            }
            return new UserManagerResponse
            {
                Message = "User did not create",
                IsSuccess = false,
                Errors = identityResult.Errors.Select(e => e.Description)
            };
        }

        public async Task<UserManagerResponse> ConfirmEmailAsync(string userId, string token)
        {
            var identityUser = await _userManager.FindByIdAsync(userId);
            if (identityUser == null)
                return new UserManagerResponse
                {
                    Message = "User not found",
                    IsSuccess = false
                };
            var decodedToken = WebEncoders.Base64UrlDecode(token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            var result = await _userManager.ConfirmEmailAsync(identityUser, normalToken);
            if (result.Succeeded)
            {
                return new UserManagerResponse
                {
                    Message = "Email confirmed successfully!",
                    IsSuccess = true
                };
            }
            return new UserManagerResponse
            {
                Message = "Email did not confirm",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }

        public Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {

            return Task.FromResult<UserManagerResponse>(null);
        }

        public async Task<UserManagerResponse> ForgetPasswordAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "No user associated with email",
                    IsSuccess = false
                };
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var encodeToken = Encoding.UTF8.GetBytes(token);
            string validToken = WebEncoders.Base64UrlEncode(encodeToken);

            string link = $"{_configuration["DomainUrl"]}/ResetPassword?email={email}&token={validToken}";

            //send email


            return new UserManagerResponse
            {
                Message = "Reset password url has been sent to the email successfully!",
                IsSuccess = true
            };

        }

        public Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model)
        {
            return Task.FromResult<UserManagerResponse>(null);
        }
    }
}
