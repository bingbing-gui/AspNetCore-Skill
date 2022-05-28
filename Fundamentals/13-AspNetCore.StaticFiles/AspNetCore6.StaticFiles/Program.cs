#define UseFileServerForDefaultDocuments

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
using Microsoft.Extensions.FileProviders;

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
/*无参数 UseStaticFiles 方法重载将 Web 根目录中的文件标记为可用
 MyStaticFiles 目录层次结构通过 StaticFiles URI 段公开 。 
 对 https://<hostname>/StaticFiles/images/red-rose.jpg 的请求将提供 red-rose.jpg 文件
 */
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
    RequestPath = "/StaticFiles"
}) ;
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion
#elif SetHTTPResponseHeaders
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
/*无参数 UseStaticFiles 方法重载将 Web 根目录中的文件标记为可用
 MyStaticFiles 目录层次结构通过 StaticFiles URI 段公开 。 
 对 https://<hostname>/StaticFiles/images/red-rose.jpg 的请求将提供 red-rose.jpg 文件
 */
var cacheMaxAgeOneWeek = (60 * 60 * 24 * 7).ToString(); ;
app.UseStaticFiles(new StaticFileOptions()
{
   OnPrepareResponse = context => 
   {
       context.Context.Response.Headers.Append(
             "Cache-Control", $"public, max-age={cacheMaxAgeOneWeek}");
   }
}) ;
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion
#elif DirectoryBrowsing
#region
using Microsoft.Extensions.FileProviders;

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
/*启用目录浏览*/
app.UseDirectoryBrowser();
/*无参数 UseStaticFiles 方法重载将 wwwroot 根目录中的文件标记为可用 */
var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath, "images"));
//var fileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath,"MyStaticFiles"));
var requestPath = "/MyImages";
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});
app.UseDirectoryBrowser(new DirectoryBrowserOptions()
{
    FileProvider = fileProvider,
    RequestPath = requestPath
});
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion
#elif DefaultFiles
#region
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseAuthorization();
app.MapDefaultControllerRoute();
app.MapRazorPages();
app.Run();
#endregion
#elif UseFileServerForDefaultDocuments
#region
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
//builder.Services.AddDirectoryBrowser();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseFileServer(enableDirectoryBrowsing: true);

app.UseAuthorization();

app.MapDefaultControllerRoute();
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