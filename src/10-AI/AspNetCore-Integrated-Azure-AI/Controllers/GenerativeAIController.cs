using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Integrated.Azure.AI.Controllers
{
    public class GenerativeAIController : Controller
    {
        // GET: /GenerativeAI
        public IActionResult Index()
        {
            return View();
        }

        // POST: /GenerativeAI/GenerateContent
        [HttpPost]
        public IActionResult GenerateContent(string input)
        {
            // Logic to generate content based on the input
            return View();
        }
    }
}