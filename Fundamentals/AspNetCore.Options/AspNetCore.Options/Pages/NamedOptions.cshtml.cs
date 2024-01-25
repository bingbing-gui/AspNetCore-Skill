using AspNetCore6.Options.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
namespace AspNetCore6.Options.Pages
{
    public class NamedOptionsModel : PageModel
    {
        public readonly IOptionsSnapshot<TopItemSettings> _optionDelegate;

        private readonly TopItemSettings _monthTopItem;
        private readonly TopItemSettings _yearTopItem;
        public NamedOptionsModel(IOptionsSnapshot<TopItemSettings> optionDelegate)
        {
            _monthTopItem = optionDelegate.Get(TopItemSettings.Year);
            _yearTopItem = optionDelegate.Get(TopItemSettings.Month);
        }
        public ContentResult OnGet()
        {
            return Content($"Month:Name {_monthTopItem.Name} \n" +
                          $"Month:Model {_monthTopItem.Model} \n\n" +
                          $"Year:Name {_yearTopItem.Name} \n" +
                          $"Year:Model {_yearTopItem.Model} \n");
        }
    }
}
