using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.DependencyInjection.Practice.Middleware
{
    public class MyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        private readonly IOperationTransient _operationTransient;
        private readonly IOperationSingleton _operationSingleton;
        public MyMiddleware(RequestDelegate next, ILogger<MyMiddleware> logger,
            IOperationTransient operationTransient,
            IOperationSingleton operationSingleton)
        {
            _next = next;
            _logger = logger;
            _operationTransient = operationTransient;
            _operationSingleton = operationSingleton;

        }
        public async Task InvokeAsync(HttpContext context, IOperationScoped operationScoped)
        {
            _logger.LogInformation("Transient: " + _operationTransient.OperationId);
            _logger.LogInformation("Scoped: " + operationScoped.OperationId);
            _logger.LogInformation("Singleton: " + _operationSingleton.OperationId);
            await _next(context);
        }
    }
    public static class MyMiddlewareExtensions
    {
        public static IApplicationBuilder UseMyMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MyMiddleware>();
        }
    }
}
