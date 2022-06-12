using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Cookie.Extensions
{
    public static class UrlHelperExtensions
    {
        public static string? GetLocalUrl(this IUrlHelper urlHelper, string url)
        {
            if (!urlHelper.IsLocalUrl(url))
            {
                return urlHelper?.Page("Index");
            }
            return url;
        }

    }
}
