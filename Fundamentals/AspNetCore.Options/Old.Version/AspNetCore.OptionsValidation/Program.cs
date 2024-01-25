#define scene3
#if scene
using AspNetCore6.OptionsValidation.Configuration;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
/*数据验证*/
builder.Services.AddOptions<MyConfigOptions>()
                .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig))
                .ValidateDataAnnotations();
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
#elif scene2
using AspNetCore6.OptionsValidation.Configuration;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
/*数据验证*/
builder.Services.AddOptions<MyConfigOptions>()
                .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig))
                .ValidateDataAnnotations()
                .Validate(myConfigOptions =>
                {
                    if (myConfigOptions.Key2 != 0)
                    {
                        return myConfigOptions.Key3 > myConfigOptions.Key2;
                    }
                    return true;
                }, "Key3 must be > than Key2.");

/*下面代码任然需要验证Key1*/
//builder.Services.PostConfigure<MyConfigOptions>(myConfigOptions =>
//{
//    myConfigOptions.Key1 = "post_configured_key1_value";
//});

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
#elif scene3

using AspNetCore6.OptionsValidation.Configuration;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
/*数据验证*/
builder.Services.Configure<MyConfigOptions>(builder.Configuration.GetSection(MyConfigOptions.MyConfig));

builder.Services.AddSingleton<IValidateOptions<MyConfigOptions>, MyConfigValidation>();

builder.Services.PostConfigure<MyConfigOptions>(myOptions =>
{
    myOptions.Key1 = "post_configured_key1_value";
});

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
#elif scene4
using AspNetCore6.OptionsValidation.Configuration;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddOptions<MyConfigOptions>()
                .Bind(builder.Configuration.GetSection(MyConfigOptions.MyConfig));

/*在对数据验证之前进行修改*/
builder.Services.PostConfigure<MyConfigOptions>(myOptions =>
{
    myOptions.Key1 = "post_configured_key1_value";
});
// </snippet_p1>


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
#endif