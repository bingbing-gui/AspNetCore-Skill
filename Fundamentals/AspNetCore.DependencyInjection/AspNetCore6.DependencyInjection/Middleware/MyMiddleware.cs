using AspNetCore6.DependencyInjection.Interfaces;

namespace AspNetCore6.DependencyInjection.Middleware
{
    #region snippet
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        private readonly IOperationTransient _transientOperation;
        private readonly IOperationSingleton _singletonOperation;
        /// <summary>
        /// 注意中间件构造函数中不能注入Scoped类型，只在能InvokeAsync方法中注入
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="transientOperation"></param>
        /// <param name="operationScoped"></param>
        /// <param name="singletonOperation"></param>
        public MyMiddleware(RequestDelegate next,
            ILogger<MyMiddleware> logger,
            IOperationTransient transientOperation,
            IOperationSingleton singletonOperation)
        {
            _logger = logger;
            _transientOperation = transientOperation;
            _singletonOperation = singletonOperation;
            _next = next;
        }

        #region snippet2
        public async Task InvokeAsync(HttpContext context,
            IOperationScoped scopedOperation)
        {
            _logger.LogInformation("Transient: " + _transientOperation.OperationId);
            _logger.LogInformation("Scoped: " + scopedOperation.OperationId);
            _logger.LogInformation("Singleton: " + _singletonOperation.OperationId);

            await _next(context);
        }
        #endregion
    }

    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
    #endregion
}
