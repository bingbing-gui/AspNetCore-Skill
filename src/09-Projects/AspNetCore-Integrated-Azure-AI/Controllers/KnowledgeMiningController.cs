using Microsoft.AspNetCore.Mvc;

namespace AspNetCore.Integrated.Azure.AI.Controllers
{
    public class KnowledgeMiningController : Controller
    {
        // GET: /KnowledgeMining
        public IActionResult Index()
        {
            return View();
        }

        // POST: /KnowledgeMining/MineKnowledge
        [HttpPost]
        public IActionResult MineKnowledge()
        {
            // Implement your knowledge mining logic here
            return View();
        }
    }
}