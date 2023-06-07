using AspNetCore.Configuration.Middlewares;
using AspNetCore.Configuration.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddSingleton<TotalUsers>();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseMiddleware<ResponseEditingMiddleware>();
app.UseMiddleware<RequestEditingMiddleware>();
app.UseMiddleware<ShortCircuitMiddleware>();
app.UseMiddleware<ContentMiddleware>();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
