using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ExceptionHandle.Pratice.Pages
{
    public class PrivacyModel : PageModel
    {
        private readonly ILogger<PrivacyModel> _logger;

        public PrivacyModel(ILogger<PrivacyModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {          // using Microsoft.AspNetCore.Diagnostics;
            var statusCodePagesFeature = HttpContext.Features.Get<IStatusCodePagesFeature>();

            if (statusCodePagesFeature != null)
            {
                statusCodePagesFeature.Enabled = false;
            }
        }
    }
}
