using AspNetCore.WebApi.HttpContenxt.Model;

namespace AspNetCore.WebApi.HttpContenxt.Service
{
    /// <summary>
    /// 在服务层访问HttpContext
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserService(IHttpContextAccessor httpContextAccessor) =>
            _httpContextAccessor = httpContextAccessor;

        public User GetCurrentUser()
        {
            ArgumentNullException.ThrowIfNull(_httpContextAccessor.HttpContext);
            ArgumentNullException.ThrowIfNull(_httpContextAccessor.HttpContext.User.Identity);
            var userName = _httpContextAccessor.HttpContext.User.Identity.Name;
            return new User();
        }
    }
}
