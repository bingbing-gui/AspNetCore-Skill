using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AspNetCore.ExceptionHandle.Pratice.Pages
{
    public class MyStatusCodeModel : PageModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public int ErrorStatusCode { get; set; }
        public string ErrorMessage { get; set; }

        public void OnGet(int code)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorStatusCode = code;
            if (ErrorStatusCode == 404)
            {
                ErrorMessage = "The requested page not found.";
            }
            else if (ErrorStatusCode == 500)
            {
                ErrorMessage = "My custom 500 error message.";
            }
            else
            {
                ErrorMessage = "An error occurred while processing your request.";
            }
        }
    }
}
