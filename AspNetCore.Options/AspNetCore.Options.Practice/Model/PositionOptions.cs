using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Text.RegularExpressions;

namespace AspNetCore.Options.Practice
{
    public class PositionOptions
    {
        public const string Position = "Position";

        public string Title { get; set; }
        public string Name { get; set; }
    }
    
}
