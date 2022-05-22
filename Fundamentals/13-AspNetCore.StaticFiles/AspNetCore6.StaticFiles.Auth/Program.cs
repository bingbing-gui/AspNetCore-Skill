using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();


builder.Services.AddAuthorization(authorizationOption =>
{
    authorizationOption.FallbackPolicy = new AuthorizationPolicyBuilder()
       .RequireAuthenticatedUser()
       .Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
/**/
app.UseHttpsRedirection();
/*
wwwroot 下的静态资产是可公开访问的,
因为在 UseAuthentication 之前会调用默认静态文件中间件 (app.UseStaticFiles();)。 
MyStaticFiles 文件夹中的静态资产需要身份验证
*/
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

//app.UseStaticFiles(new StaticFileOptions()
//{
//    FileProvider = new PhysicalFileProvider(
//        Path.Combine(builder.Environment.ContentRootPath, "MyStaticFiles")
//        ),
//    RequestPath = "/StaticFiles"
//});

app.MapRazorPages();

app.Run();
