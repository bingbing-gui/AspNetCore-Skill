using AspNetCore.Authentication.AuthorizationRequirements;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, configureOptions =>
    {
        configureOptions.Cookie.Name = "Grandmas.Cookie";
        configureOptions.LoginPath = "/Home/Authenticate";
    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Claim.DoB", policyBuilder =>
    {
        policyBuilder.RequireCustomClaim(ClaimTypes.DateOfBirth);
    });
});
builder.Services.AddScoped<IAuthorizationHandler, CustomRequireClaimHandler>();

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
