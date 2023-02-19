using Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.IdentityPolicy
{
    public class CustomPasswordPolicy : PasswordValidator<AppUser>
    {
        public override async Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string? password)
        {
            //验证Password 规则
            IdentityResult result = await base.ValidateAsync(manager, user, password);
            List<IdentityError> errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();
            if (password!.ToLower().Contains(user!.UserName!.ToLower()))
            {
                errors.Add(new IdentityError
                {
                    Description = "密码不能包含用户名"
                });
            }
            if (password.Contains("123"))
            {
                errors.Add(new IdentityError
                {
                    Description = "密码不能包含123数字序列"
                });
            }
            return errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray());
        }
    }
}
