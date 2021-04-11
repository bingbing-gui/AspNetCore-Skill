using AspNetCore.ExceptionHandle.Exception;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ExceptionHandle.Controllers
{
    [Route("/error")]
    public class ErrorController : Controller
    {

        public IActionResult Index()
        {
            var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            var ex = exceptionHandlerPathFeature?.Error;
            var knowException = ex as IKnowException;
            if (knowException == null)
            {
                
            }
            return View();
        }
    }
}
