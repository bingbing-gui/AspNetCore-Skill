using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ExceptionHandle.Pratice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.UseStartup<Startup>();
                    //webBuilder.UseStartup<StartupLambda>();
                    //webBuilder.UseStartup<StartupUseStatusCodeOriginalPages>();
                    //webBuilder.UseStartup<StartupUseStatusCodeFormat>();
                    //webBuilder.UseStartup<StartupUseStatusCodeLambda>();
                    webBuilder.UseStartup<StartupUseStatusCodeWithRedirects>();
                    //webBuilder.UseStartup<StartupUseStatusCodeReExecute>();
                });
    }
}
