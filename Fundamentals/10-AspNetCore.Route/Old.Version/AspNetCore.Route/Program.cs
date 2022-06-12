using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Route
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
                    webBuilder.UseStartup<Startup>();
                    //webBuilder.UseStartup<RouteTemplateStartup>();
                    //webBuilder.UseStartup<AuthorizationStartup>();
                    //webBuilder.UseStartup<EndpointInspectorStartup>();
                    //webBuilder.UseStartup<MiddlewareFlowStartup>(); 
                   // webBuilder.UseStartup<IntegratedMiddlewareStartup>();
                });
    }
}
