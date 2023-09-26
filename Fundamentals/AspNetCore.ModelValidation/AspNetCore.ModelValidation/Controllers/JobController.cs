using AspNetCore.ModelValidation.Models;
using Microsoft.AspNetCore.Mvc;
namespace AspNetCore.ModelValidation.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(JobApplication jobApplication)
        {
            return View("Accepted", jobApplication);
        }
    }
}
