using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Integrated.Azure.AI.Controllers
{
    public class VisionController : Controller
    {
        // GET: /Vision
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Vision/AnalyzeImage
        [HttpPost]
        public IActionResult AnalyzeImage()
        {
            // Logic for analyzing an image goes here
            return View();
        }
    }
}