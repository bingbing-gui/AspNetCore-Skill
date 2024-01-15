using AspNetCore6.Middleware.Services;

namespace AspNetCore6.Middleware.Middleware
{
    public class MyCustomMiddleware
    {
        private readonly RequestDelegate _next;
        public MyCustomMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IMessageWriter messageWriter)
        {
            messageWriter.Write(DateTime.Now.Ticks.ToString());
            await _next(context);
        }
    }
    public static class MyCustomMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyCustomMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
