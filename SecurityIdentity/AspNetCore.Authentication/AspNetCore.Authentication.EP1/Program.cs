using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configureOptions =>
    {
        configureOptions.Cookie.Name = "Grandmas.Cookie";
        configureOptions.LoginPath = "/Home/Authenticate";
    });

builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();
//who you are?
app.UseAuthentication();
//are you allowed
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
