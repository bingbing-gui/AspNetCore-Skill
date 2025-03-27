using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Integrated.Azure.AI.Controllers
{
    public class SpeechServiceController : Controller
    {
        // GET: /SpeechService
        public IActionResult Index()
        {
            return View();
        }

        // POST: /SpeechService/RecognizeSpeech
        [HttpPost]
        public IActionResult RecognizeSpeech()
        {
            // Implement speech recognition logic here
            return View();
        }
    }
}