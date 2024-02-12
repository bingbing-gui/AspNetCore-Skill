using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Logging.Debug;

var builder = WebApplication.CreateBuilder(args);

#region 在代码中对日志进行刷选, 通常，日志级别应在配置中指定，而不是在代码中指定
builder.Logging.AddFilter((provider, category, logLevel) =>
{
    if (provider.Contains("ConsoleLoggerProvider") // 日志提供程序
        && category.Contains("Controller") // 日志类别
        && logLevel >= LogLevel.Information)// 日志级别
    {
        return true;
    }
    else if (provider.Contains("ConsoleLoggerProvider")
        && category.Contains("Microsoft")
        && logLevel >= LogLevel.Information)
    {
        return true;
    }
    else
    {
        return false;
    }
});

builder.Logging.AddFilter("System", LogLevel.Debug);
builder.Logging.AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Information);
builder.Logging.AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Trace);

#endregion 
#region 在代码中设置日志级别 通常，日志级别应在配置中指定，而不是在代码中指定。
builder.Logging.SetMinimumLevel(LogLevel.Warning);
#endregion
#region 第一种方法
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
#endregion
builder.Host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
{
    
   
});
#region 第二种方法
builder.Host.ConfigureLogging((hostBuilderContext, logging) =>
{    
    logging.ClearProviders();
    logging.AddConsole();
});
#endregion


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
