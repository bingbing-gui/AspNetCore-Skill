using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AspNetCore.Log.Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host =CreateHostBuilder(args).Build();
            /*
             在Program 中创建日志
             */
            var logger = host.Services.GetRequiredService<ILogger<Program>>();
            logger.LogInformation("Seeded the database.");
            host.Run();
        }
        /*
        1. CreateDefaultBuilder 添加了下面 providers
           1.1 Console
           1.2 Debug
           1.3 EventSource
           1.4 EventLog:在window 系统下
        */
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    /*
                     1.移除所有的ILoggerProvider
                     2.添加Console logging Provider
                     */
                    logging.ClearProviders();
                    logging.AddConsole();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
