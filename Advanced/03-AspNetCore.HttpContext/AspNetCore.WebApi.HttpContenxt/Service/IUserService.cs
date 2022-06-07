using AspNetCore.WebApi.HttpContenxt.Model;

namespace AspNetCore.WebApi.HttpContenxt.Service
{
    public interface IUserService
    {
        User GetCurrentUser();
    }
}
