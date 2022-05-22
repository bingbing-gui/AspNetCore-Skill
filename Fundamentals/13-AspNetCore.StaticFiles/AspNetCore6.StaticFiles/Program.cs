#define ServeFilesOutsideOfWebRoot

#if ServeStaticFiles
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var app=builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
/*无参数 UseStaticFiles 方法重载将 Web 根目录中的文件标记为可用*/
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();

#elif ServeFilesOutsideOfWebRoot
#region
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
/*无参数 UseStaticFiles 方法重载将 Web 根目录中的文件标记为可用*/
app.UseStaticFiles(new StaticFileOptions() 
{

});
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion
#elif WebRoot
#region
var builder = WebApplication.CreateBuilder(new WebApplicationOptions() 
{
    /*如果不设置EnvironmentName 参数,在非开发环境下，静态资源指向WebRootPath*/
    Args = args,
    EnvironmentName=Environments.Development,
    WebRootPath= "wwwroot-custom"
});
// Add services to the container.
builder.Services.AddRazorPages();

var app = builder.Build();

app.Logger.LogInformation("ASPNETCORE_ENVIRONMENT: {env}",
      Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));

app.Logger.LogInformation("app.Environment.IsDevelopment(): {env}",
      app.Environment.IsDevelopment().ToString());

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseDefaultFiles();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
#endregion
#endif