using AspNetCore.DependencyInjection.Interfaces;

namespace AspNetCore.DependencyInjection.Services
{
    public class OperationServie : IOperationService
    {
        private readonly ILogger<OperationServie> _logger;

        public OperationServie(
            IOperationTransient operationTransient,
            IOperationScoped operationScoped,
            IOperationSingleton operationSingleton,
            ILogger<OperationServie> logger)
        {
            TransientOperation = operationTransient;
            ScopedOperation = operationScoped;
            SingletonOperation = operationSingleton;
            _logger = logger;
        }
        
        public IOperationTransient TransientOperation { get; }
        
        public IOperationScoped ScopedOperation { get; }
        
        public IOperationSingleton SingletonOperation { get; }

        public void TestLifetime()
        {
            _logger.LogInformation("From Service Transient: " + TransientOperation.OperationId);
            _logger.LogInformation("From Service Scoped: " + ScopedOperation.OperationId);
            _logger.LogInformation("From Service Singleton: " + SingletonOperation.OperationId);
        }

    }
}
