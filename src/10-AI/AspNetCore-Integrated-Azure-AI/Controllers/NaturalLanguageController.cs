using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Integrated.Azure.AI.Controllers
{
    public class NaturalLanguageController : Controller
    {
        // GET: /NaturalLanguage
        public IActionResult Index()
        {
            return View();
        }

        // POST: /NaturalLanguage/ProcessText
        [HttpPost]
        public IActionResult ProcessText(string inputText)
        {
            // Implement your natural language processing logic here
            // For now, just return the input text to the view
            ViewBag.ProcessedText = inputText; // Example of passing data to the view
            return View("Index");
        }
    }
}