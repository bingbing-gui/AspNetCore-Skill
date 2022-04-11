#define 在应用启动时解析服务 
/* 
 * 1.依赖注入
 * 2.依赖注入使用内置日志记录API 
 * 3.OneInterfaceMapMultipleDerived
 * 4.DILifeTime
 * 5.在应用启动时解析服务 */

using AspNetCore6.DependencyInjection.Interfaces;
using AspNetCore6.DependencyInjection.Middleware;
using AspNetCore6.DependencyInjection.Models;
using AspNetCore6.DependencyInjection.Services;
#if 依赖注入
#region
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IMyDependency, MyDependency>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion
#elif 依赖注入使用内置日志记录API 
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<IMyDependency, MyDependency2>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#elif 一个接口多个实例派生
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
//一个接口映射到多个子类
builder.Services.AddScoped<IMyDependency, MyDependency>();
builder.Services.AddScoped<IMyDependency, MyDependency2>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#elif 依赖注入声明周期
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();

//一个接口映射到多个子类
builder.Services.AddScoped<IMyDependency,MyDependency>();
builder.Services.AddTransient<IOperationTransient,Operation>();
builder.Services.AddScoped<IOperationScoped,Operation>();
builder.Services.AddSingleton<IOperationSingleton,Operation>();
builder.Services.AddScoped<IOperationService, OperationServie>();

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseMyMiddleware();
app.MapRazorPages();
app.Run();
#elif 在应用启动时解析服务
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
//一个接口映射到多个子类
builder.Services.AddScoped<IMyDependency,MyDependency>();


var app = builder.Build();
using (var serviceScope=app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var myDependency = services.GetRequiredService<IMyDependency>();
    myDependency.WriteMessage("Call services from main");
}
app.MapGet("/", () => "Hello World!");
app.Run();

#endif
