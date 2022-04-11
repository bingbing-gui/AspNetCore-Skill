using AspNetCore6.FourWaysToDisposeIDisposables.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// other services

// these will be disposed
builder.Services.AddTransient<TransientCreatedByContainer>();
builder.Services.AddScoped(ctx => new ScopedCreatedByFactory());
builder.Services.AddSingleton<SingletonCreatedByContainer>();
// this one won't be disposed
builder.Services.AddSingleton(new SingletonAddedManually());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var singletonAddedManually = app.Services.GetService<SingletonAddedManually>();
/*
When the application is running in the console: CTRL + C.
    For example when running through dotnet run or running the compiled executable directly.
When the application is running in IIS: 
    Stopping the website or the application pool.
When the application is running as a Windows Service: 
    Stopping the service
 */
app.Lifetime.ApplicationStopping.Register((obj =>
{
    if (obj != null)
        ((IDisposable)obj).Dispose();

}), singletonAddedManually);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

