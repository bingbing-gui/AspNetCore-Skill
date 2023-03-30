using AspNetCore.DependencyInjection.Interfaces;

namespace AspNetCore.DependencyInjection.Models
{
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Operation()
        {
            OperationId = Guid.NewGuid().ToString()[^4..];
        }
        public string OperationId { get; }
    }
}
