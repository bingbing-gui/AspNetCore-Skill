using System.Net;


namespace AspNetCore.Startup.Middleware
{
    public class RequestSetOptionsMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestSetOptionsMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            Console.WriteLine("Start RequestSetOptionsMiddleware");
            var option = httpContext.Request.Query["option"];
            if (!string.IsNullOrWhiteSpace(option))
            {
                httpContext.Items["option"] = WebUtility.HtmlEncode(option);
            }
            await _next(httpContext);

            Console.WriteLine("End RequestSetOptionsMiddleware");
        }
    }
}