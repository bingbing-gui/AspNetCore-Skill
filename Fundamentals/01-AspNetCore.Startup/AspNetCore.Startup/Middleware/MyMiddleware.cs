namespace AspNetCore.Startup.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        public MyMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Start MyMiddleware ");
            await _next(context);
            Console.WriteLine("End MyMiddleware");
        }
    }
}
