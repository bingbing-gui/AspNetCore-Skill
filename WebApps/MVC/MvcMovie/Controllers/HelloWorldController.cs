using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcMovie.Controllers
{
    public class HelloWorldController : Controller
    {
        // 
        // GET: /HelloWorld/

        public IActionResult Index()
        {
            return View();
        }

        // GET: /HelloWorld/Welcome/ 
        // Requires using System.Text.Encodings.Web;
        public IActionResult Welcome(string name, int numTimes = 1)
        {
            ViewData["Message"] = "Hello" + name;
            ViewData["numTimes"] = numTimes;
            return View();
            //return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {numTimes}");
        }
        // GET: /HelloWorld/Welcome2/ 
        //Id 为RouteData上
        // Requires using System.Text.Encodings.Web;
        public string Welcome2(string name, int id = 1)
        {
            return HtmlEncoder.Default.Encode($"Hello {name}, NumTimes is: {id}");
        }
    }
}
