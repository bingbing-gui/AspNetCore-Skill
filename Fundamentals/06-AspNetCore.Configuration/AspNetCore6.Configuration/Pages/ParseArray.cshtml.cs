using AspNetCore6.Configuration.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace AspNetCore6.Configuration.Pages
{
    public class ParseArrayModel : PageModel
    {
        private readonly IConfiguration _configuration;
        public ArrayExample? _array { get; private set; }
        public ParseArrayModel(IConfiguration configuration)
        { 
            _configuration = configuration;
        }
        public ContentResult OnGet()
        {
            _array=_configuration.GetSection("array").Get<ArrayExample>();
            if (_array == null)
            {
                throw new ArgumentNullException(nameof(_array));
            }
            var stringBuilder = new StringBuilder();
            for (int j = 0; j < _array.Entries?.Length; j++)
            {
                stringBuilder.Append($"Index: {j}  Value:  {_array.Entries[j]} \n");
            }
            //通一个IConfiguration 可以跨不同文件访问
            var myKeyValue = _configuration["MyKey"];
            var title = _configuration["Position:Title"];
            var name = _configuration["Position:Name"];
            var defaultLogLevel = _configuration["Logging:LogLevel:Default"];
            stringBuilder.Append(myKeyValue);
            stringBuilder.Append(title);
            stringBuilder.Append(name);
            stringBuilder.Append(defaultLogLevel);

            return Content(stringBuilder.ToString());
        }
    }
}
