using AspNetCore.ChangeToToken.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ChangeToToken.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IConfiguration _config;
        private readonly IConfigurationMonitor _monitor;

        private readonly Dictionary<string, string> _styleDict = new Dictionary<string, string>()
        {
            { "Trace", "default" },
            { "Debug", "success" },
            { "Information", "info" },
            { "Warning", "warning" },
            { "Error", "primary" },
            { "Critical", "danger" }
        };
        public string DefaultLogLevel { get; private set; }
        public string SystemLogLevel { get; private set; }
        public string MicrosoftLogLevel { get; private set; }

        public HtmlString FileContents { get; private set; }

        public IndexModel(IConfiguration config, IConfigurationMonitor monitor, ILogger<IndexModel> logger)
        {
            _config = config;
            _monitor = monitor;
            _logger = logger;
        }

        [TempData]
        public string CurrentState { get; set; }

        public void OnGet()
        {
            DefaultLogLevel = _config["Logging:LogLevel:Default"];
            SystemLogLevel = _config["Logging:LogLevel:System"];
            MicrosoftLogLevel = _config["Logging:LogLevel:Microsoft"];

            ViewData["IncludeScopesStyle"] = string.Equals(_config["Logging:IncludeScopes"], "True", StringComparison.OrdinalIgnoreCase) ? "success" : "danger";
            ViewData["DefaultLogLevelStyle"] = _styleDict[_config["Logging:LogLevel:Default"]];
            ViewData["SystemLogLevelStyle"] = _styleDict[_config["Logging:LogLevel:System"]];
            ViewData["MicrosoftLogLevelStyle"] = _styleDict[_config["Logging:LogLevel:Microsoft"]];

            CurrentState = _monitor.CurrentState;
        }
        public IActionResult OnPostStartMonitoring()
        {
            _monitor.MonitoringEnabled = true;
            _monitor.CurrentState = "Monitoring!";

            return RedirectToPage();
        }
        public IActionResult OnPostStopMonitoring()
        {
            _monitor.MonitoringEnabled = false;
            _monitor.CurrentState = "Not monitoring";

            return RedirectToPage();
        }

    }
}
