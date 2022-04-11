using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore6.ObjectDisposeFromContainer.Pages
{
    public class IndexModel : PageModel
    {       
        private readonly Service1 _service1;
        private readonly Service2 _service2;
        private readonly IService3 _service3;
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(
            Service1 service1,
            Service2 service2,
            IService3 service3,
            ILogger<IndexModel> logger)
        {
            _service1 = service1;
            _service2 = service2;
            _service3 = service3;
            _logger = logger;
        }

        public void OnGet()
        {
            _service1.Write("Service1.OnGet()");
            _service2.Write("Service2.OnGet()");
            _service3.Write("Service3.OnGet()");
        }
    }
}