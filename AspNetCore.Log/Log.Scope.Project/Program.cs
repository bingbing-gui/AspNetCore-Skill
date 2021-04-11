using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Log.Scope.Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //1. 构建配置框架
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddCommandLine(args);//method from Microsoft.Extensions.Configuration.CommandLine
            configBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);//method from Microsoft.Extensions.Configuration.Json
            IConfigurationRoot config = configBuilder.Build();
            //2. 构建日志框架
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(p => config);//使用工厂模式将配置对象注册到容器里
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));
                builder.AddConsole();
                builder.AddDebug();
            });
            var serviceProvider=serviceCollection.BuildServiceProvider();
            var logger=serviceProvider.GetService<ILogger<Program>>();
            //Log Scope 一般用唯一的ID来标识一个Scope
            using (logger.BeginScope("ScopeId:{scopeId}",Guid.NewGuid()))
            {
                logger.LogInformation("Scope Information");
                logger.LogDebug("Scope Debug");
                logger.LogTrace("Scope Trace");
            }
            Console.ReadLine();
        }
    }
}
