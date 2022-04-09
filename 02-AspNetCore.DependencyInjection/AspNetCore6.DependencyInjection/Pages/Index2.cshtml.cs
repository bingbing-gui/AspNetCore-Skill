using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AspNetCore6.DependencyInjection.Interfaces;

namespace AspNetCore6.DependencyInjection.Pages
{
    public class Index2Model : PageModel
    {
        private readonly IMyDependency _myDependency;
        private readonly ILogger<Index2Model> _logger;

        public Index2Model(IMyDependency myDependency,ILogger<Index2Model> logger)
        {
            _myDependency = myDependency;
            _logger = logger;
        }

        public void OnGet()
        {
            _myDependency.WriteMessage("Index2Model");
        }
    }
}