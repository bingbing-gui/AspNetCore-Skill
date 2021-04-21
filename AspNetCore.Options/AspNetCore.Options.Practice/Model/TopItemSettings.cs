using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Options.Bind.Practice
{
    public class TopItemSettings
    {
        public const string Month = "Month";
        public const string Year = "Year";

        public string Name { get; set; }
        public string Model { get; set; }
    }
}
