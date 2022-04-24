#define WebService

using AspNetCore6.GenericHost.Services;
/*
CreateDefaultBuilder 方法:
1.将内容目录设置为有GetCurrentDirectory返回的路径。
2.通过一下项加载主机配置
  2.1 前缀为 DOTNET_ 的环境变量。
  2.1 命令行参数。
3.通过以下对象记载应用配置：
  3.1 appsettings.json.
  3.2 appsettings.{Environment}.json
  3.3 应用在环境中运行时的用户机密。
  3.4 环境变量。
  3.5 命令行参数。
4.添加一下日志提供程序:
  4.1 控制台
  4.2 调试
  4.3 EventSource
  4.4 EventLog(仅当在Windows上运行时)
5.当环境为"开发"时，启用范围验证和依赖关系验证
  
ConfigureWebHostDefaults 方法

1.从前缀为 ASPNETCORE_ 的环境变量加载主机配置。
2.使用应用的托管配置提供程序将 Kestrel 服务器设置为 web 服务器并对其进行配置。 
  有关 Kestrel 服务器的默认选项，请参阅Kestrel
3.添加主机筛选中间件。
4.如果 等于 true，则添加转接头中间件。
5.支持 IIS 集成。 有关 IIS 默认选项，请参阅使用 IIS 在 Windows 上托管 ASP.NET Core。 
 */
#if GenericService
await Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddHostedService<SampleHostedService>();
    })
    .Build()
    .RunAsync();

#elif WebService
await Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>();
    })
    .Build()
    .RunAsync();

#endif
