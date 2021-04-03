using AspNetCore.DependencyInjection.Practice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.DependencyInjection.Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOperationTransient _transientOperation;
        private readonly IOperationSingleton _singletonOperation;
        private readonly IOperationScoped _scopedOperation;
        private IConfigurationRoot ConfigRoot;
        private readonly Service1 _service1;
        private readonly Service2 _service2;
        private readonly IService3 _service3;
        public HomeController(IOperationTransient operationTransient,
            IOperationScoped operationScoped,
            IOperationSingleton operationSingleton,
            Service1 service1,
            Service2 service2,
            IService3 service3, IConfiguration configuration,
            ILogger<HomeController> logger)
        {
            _transientOperation = operationTransient;
            _singletonOperation = operationSingleton;
            _scopedOperation = operationScoped;
            _service1 = service1;
            _service2 = service2;
            _service3 = service3;
            _logger = logger;
            ConfigRoot = (IConfigurationRoot)configuration;
        }

        public IActionResult Index()
        {
            _logger.LogInformation("Transient: " + _transientOperation.OperationId);
            _logger.LogInformation("Scoped: " + _scopedOperation.OperationId);
            _logger.LogInformation("Singleton: " + _singletonOperation.OperationId);

            _service1.Write("Service1.Dispose");
            _service2.Write("Service2.Dispose");
            _service3.Write("Service3.Dispose");

            string str = "";
            foreach (var provider in ConfigRoot.Providers.ToList())
            {
                str += provider.ToString() + "\n";
            }
            _logger.LogInformation("configuration: " + str);
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
