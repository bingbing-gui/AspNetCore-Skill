using Log.Project.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Log.Project
{
    class Program
    {
        static void Main(string[] args)
        {
            //1.下面三行代码可以实现从文件中读取配置
            IConfigurationBuilder configBuilder = new ConfigurationBuilder();
            configBuilder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            var config = configBuilder.Build();

            //2.将配置对象注入到容器中.
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton<IConfiguration>(p => config);//用工厂模式将配置对象注入到容器
            serviceCollection.AddScoped<IOrderService, OrderService>();
            //3.添加Log 服务
            serviceCollection.AddLogging(builder =>
            {
                builder.AddConfiguration(config.GetSection("Logging"));//2.添加Log 配置
                builder.AddConsole();//1.添加Console 文件提供程序
            });
            //4.创建ServiceProvider 提供程序
            IServiceProvider service = serviceCollection.BuildServiceProvider();
            CreateLogger(service);
            CreateService(service);
            Console.ReadLine();
        }
        static void CreateLogger(IServiceProvider service)
        {
            //5.日志对象的获取.
            //5.1 获取ILoggerFactory对象
            //5.2创建Logger 对象.CreateLogger参数为log 名称
            ILoggerFactory loggerFactory = service.GetService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("logger");
            logger.LogDebug(1001, "log debug");
            logger.LogInformation(2001, "log information");
            logger.LogError(3001, "log error");
        }
        static void CreateService(IServiceProvider service)
        {
            IOrderService orderService = service.GetService<IOrderService>();
            orderService.WriteLog("this log from service");
        }

    }
}
