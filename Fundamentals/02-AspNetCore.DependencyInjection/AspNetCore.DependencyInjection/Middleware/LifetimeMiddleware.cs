using AspNetCore.DependencyInjection.Interfaces;

namespace AspNetCore.DependencyInjection.Middleware
{
    public class LifetimeMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IOperationTransient _operationTransient;
        private readonly IOperationSingleton _operationSingleton;
        private readonly ILogger<LifetimeMiddleware> _logger;
        public LifetimeMiddleware(
                RequestDelegate next,
                IOperationTransient operationTransient,
                IOperationSingleton operationSingleton,
                ILogger<LifetimeMiddleware> logger
                )
        {
            _next = next;
            _operationTransient = operationTransient;
            _operationSingleton = operationSingleton;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context,
            IOperationScoped scopedOperation)
        {
            _logger.LogInformation("Transient: " + _operationTransient.OperationId);
            _logger.LogInformation("Scoped: " + scopedOperation.OperationId);
            _logger.LogInformation("Singleton: " + _operationSingleton.OperationId);
            await _next(context);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseLifetimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<LifetimeMiddleware>();
        }
    }
}
