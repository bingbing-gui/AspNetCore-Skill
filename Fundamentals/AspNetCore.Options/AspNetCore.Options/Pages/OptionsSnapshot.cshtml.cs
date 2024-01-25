using AspNetCore6.Options.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AspNetCore6.Options.Pages
{
    public class OptionsSnapshotModel : PageModel
    {
        private readonly IOptionsSnapshot<MyOptions> _optionsDelegate;
        public OptionsSnapshotModel(IOptionsSnapshot<MyOptions> optionsDelegate)
        {
            _optionsDelegate = optionsDelegate;
        }

        public ContentResult OnGet()
        {
            return Content($"Option1: {_optionsDelegate.Value.Option1} \n" +
                         $"Option2: {_optionsDelegate.Value.Option2}");
        }
    }
}
