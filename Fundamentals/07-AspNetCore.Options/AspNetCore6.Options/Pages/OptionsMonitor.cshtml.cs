using AspNetCore6.Options.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AspNetCore6.Options.Pages
{
    public class OptionsMonitorModel : PageModel
    {
        private readonly IOptionsMonitor<MyOptions> _optionsDelegate;

        public OptionsMonitorModel(IOptionsMonitor<MyOptions> optionsDelegate)
        {
            _optionsDelegate = optionsDelegate;
        }
        public ContentResult OnGet()
        {
            return Content($"Option1: {_optionsDelegate.CurrentValue.Option1} \n" +
                            $"Option2: {_optionsDelegate.CurrentValue.Option2}");
        }
    }
}
