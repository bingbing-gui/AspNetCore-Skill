using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Integrated.Azure.AI.Controllers
{
    public class DocumentIntelligenceController : Controller
    {
        // GET: DocumentIntelligence
        public IActionResult Index()
        {
            return View();
        }

        // POST: DocumentIntelligence/AnalyzeDocument
        [HttpPost]
        public IActionResult AnalyzeDocument()
        {
            // Logic for analyzing a document goes here
            return View();
        }
    }
}