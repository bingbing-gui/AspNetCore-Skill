// See https://aka.ms/new-console-template for more information
using BackgroundTask.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        //services.AddSingleton<MonitorLoop>();
        //services.AddSingleton<IBackgroundTaskQueue>(ctx =>
        //{
        //    if (!int.TryParse(hostContext.Configuration["QueueCapacity"], out var queueCapacity))
        //        queueCapacity = 100;
        //    return new BackgroundTaskQueue(queueCapacity);
        //});
        //services.AddHostedService<QueuedHostedService>();
        services.AddHostedService<TimedHostedService>();
        #region
        services.AddHostedService<ConsumeScopedServiceHostedService>();
        services.AddScoped<IScopedProcessingService, ScopedProcessingService>();
        #endregion
    })
    .Build();

await host.StartAsync();
#region snippet4
//var monitorLoop = host.Services.GetRequiredService<MonitorLoop>();
//monitorLoop.StartMonitorLoop();
#endregion

await host.WaitForShutdownAsync();