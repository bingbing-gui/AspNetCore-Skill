namespace AspNetCore.Configuration.Middlewares
{
    public class ContentMiddleware
    {
        private RequestDelegate _nextDelegate;
        public ContentMiddleware(RequestDelegate next)
        {
            _nextDelegate = next;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/middleware")
            {
                await httpContext.Response.WriteAsync("这是Context 中间件");
            }
            else
            {
                _nextDelegate(httpContext);
            }
        }
    }
}
