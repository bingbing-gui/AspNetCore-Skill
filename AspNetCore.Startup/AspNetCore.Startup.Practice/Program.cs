using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;

namespace AspNetCore.Startup.Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
        /// <summary>
        /// 应用程序启动顺序：
        /// 1.ConfigureWebHostDefaults
        /// 备注:创建IWebHostBuilder并为其创建默认配置
        /// 2.ConfigureHostConfiguration
        /// 备注:是配置IHostBuilder所需要的配置
        /// 3.ConfigureAppConfiguration
        /// 备注:应用程序组件所需要的配置
        /// 4.ConfigureServices or Startup.ConfigureServices
        /// 5.Startup.Configure
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Console.WriteLine("ConfigureWebHostDefaults");
                    ///webBuilder.UseStartup<Startup>();
                    //替换startup 类
                    webBuilder.ConfigureServices(services =>
                    {
                        Console.WriteLine("Startup.ConfigureServices");
                        services.AddControllers();
                        services.AddSwaggerGen(c =>
                        {
                            c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCore.Startup.Practice", Version = "v1" });
                        });
                    });
                    webBuilder.Configure(app =>
                    {
                        Console.WriteLine("Startup.Configure");
                        app.UseSwagger();
                        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCore.Startup.Practice v1"));
                        app.UseHttpsRedirection();
                        app.UseRouting();
                        app.UseAuthorization();
                        app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllers();
                        });
                    });

                })
                .ConfigureHostConfiguration(builder =>
                {
                    
                    Console.WriteLine("ConfigureHostConfiguration");
                })
                .ConfigureAppConfiguration(builder =>
                {
                    Console.WriteLine("ConfigureAppConfiguration");
                })
                .ConfigureServices(serviceCollection =>
                {
                    Console.WriteLine("ConfigureServices");
                });


    }
}
