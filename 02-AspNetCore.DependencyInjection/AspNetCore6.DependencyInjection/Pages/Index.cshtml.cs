using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetCore6.DependencyInjection.Interfaces;

namespace AspNetCore6.DependencyInjection.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IOperationTransient _operationTransient;
        private readonly IOperationScoped _operationScoped;
        private readonly IOperationSingleton _operationSingleton;
        private readonly ILogger<IndexModel> _logger;
        private readonly IOperationService _operationService;
        public IndexModel(
            IOperationService operationService,
            IOperationTransient operationTransient,
            IOperationScoped operationScoped,
            IOperationSingleton operationSingleton,
            ILogger<IndexModel> logger)
        {
            _operationService= operationService;
            _operationTransient = operationTransient;
            _operationScoped = operationScoped;
            _operationSingleton = operationSingleton;
            _logger = logger;
        }

        public void OnGet()
        {
            _logger.LogInformation("From Control Transient: " + _operationTransient.OperationId);
            _logger.LogInformation("From Control Scoped: " + _operationScoped.OperationId);
            _logger.LogInformation("From Control Singleton: " + _operationSingleton.OperationId);
            _operationService.TestLifetime();
        }
    }
}