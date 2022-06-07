using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace AspNetCore6.OptionsValidation.Configuration
{
    public class MyConfigValidation : IValidateOptions<MyConfigOptions>
    {
        private readonly MyConfigOptions _myConfigOptions;
        public MyConfigValidation(IConfiguration configuration)
        {
            _myConfigOptions = configuration.GetSection(MyConfigOptions.MyConfig).Get<MyConfigOptions>();
        }
        public ValidateOptionsResult Validate(string name, MyConfigOptions options)
        {
            string? vor = null;
            var rx = new Regex(@"^[a-zA-Z''-'\s]{1,40}$");
            var match = rx.Match(options.Key1!);
            if (string.IsNullOrEmpty(match.Value))
            {
                vor = $"{options.Key1} doesn't match RegEx \n";
            }
            if (options.Key2 < 0 || options.Key2 > 1000)
            {
                vor = $"{options.Key2} doesn't match Range 0 - 1000 \n";
            }

            if (_myConfigOptions.Key2 != default)
            {
                if (_myConfigOptions.Key3 <= _myConfigOptions.Key2)
                {
                    vor += "Key3 must be > than Key2.";
                }
            }
            if (vor != null)
            {
                return ValidateOptionsResult.Fail(vor);
            }
            return ValidateOptionsResult.Success;
        }
    }
}
