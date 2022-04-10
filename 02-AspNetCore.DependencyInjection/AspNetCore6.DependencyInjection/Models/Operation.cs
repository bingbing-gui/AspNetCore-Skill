using AspNetCore6.DependencyInjection.Interfaces;

namespace AspNetCore6.DependencyInjection.Models
{
    #region snippet1
    public class Operation : IOperationTransient, IOperationScoped, IOperationSingleton
    {
        public Operation()
        {
            OperationId = Guid.NewGuid().ToString()[^4..];
        }

        public string OperationId { get; }
    }
    #endregion
}
