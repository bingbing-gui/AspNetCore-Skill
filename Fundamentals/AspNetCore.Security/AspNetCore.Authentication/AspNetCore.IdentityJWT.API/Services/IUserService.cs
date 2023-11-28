using AspNetCore.IdentityJWT.API.ViewModel;

namespace AspNetCore.IdentityJWT.API.Services
{
    public interface IUserService
    {

        Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model);

        Task<UserManagerResponse> LoginUserAsync(LoginViewModel model);
       
        Task<UserManagerResponse> ConfirmEmailAsync(string userId,string token);

        Task<UserManagerResponse> ForgetPasswordAsync(string email);

        Task<UserManagerResponse> ResetPasswordAsync(ResetPasswordViewModel model);
    }
}
