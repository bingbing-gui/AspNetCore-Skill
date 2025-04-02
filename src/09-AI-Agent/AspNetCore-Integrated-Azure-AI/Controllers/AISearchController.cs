using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Integrated.Azure.AI.Controllers
{
    public class AISearchController : Controller
    {
        // GET: /AISearch
        public IActionResult Index()
        {
            return View();
        }

        // POST: /AISearch/Search
        [HttpPost]
        public IActionResult Search(string query)
        {
            // Implement search logic here
            return View();
        }
    }
}
