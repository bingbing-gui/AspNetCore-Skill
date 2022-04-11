using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetCore6.DependencyInjection.Interfaces;
using AspNetCore6.DependencyInjection.Services;
using System.Diagnostics;

namespace AspNetCore6.DependencyInjection.Pages
{
    public class Index2Model : PageModel
    {
        private readonly IMyDependency _myDependency;
        private readonly IEnumerable<IMyDependency> _myDependencies;
        private readonly ILogger<Index2Model> _logger;

        public Index2Model(IMyDependency myDependency,
            IEnumerable<IMyDependency> myDependencies,
            ILogger<Index2Model> logger)
        {
            _myDependency = myDependency;
            _myDependencies = myDependencies;

            Trace.Assert(myDependency is MyDependency2);
            var dependencyArray = myDependencies.ToArray();
            Trace.Assert(dependencyArray[0] is MyDependency);
            Trace.Assert(dependencyArray[1] is MyDependency2);


            _logger = logger;
        }

        public void OnGet()
        {
            _myDependency.WriteMessage("Index2Model.OnGet()");
        }
    }
}