using AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");


//注册DBContext
builder.Services.AddDbContext<ApplicationDbContext>(optionsAction =>
{
    optionsAction.UseSqlServer(connection);
});
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
//添加Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>(identityOption =>
{
    identityOption.Password.RequiredLength = 4;
    identityOption.Password.RequireDigit = false;
    identityOption.Password.RequireNonAlphanumeric = false;
    identityOption.Password.RequireUppercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(configure =>
{
    configure.Cookie.Name = "Identity.Cookie";
    configure.LoginPath = "/Home/Login";
    configure.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    configure.SlidingExpiration = true;
});
builder.Services.AddControllersWithViews();


var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

/*初始化数据库*/

using (var serviceScope = app.Services.CreateScope())
{
    serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.EnsureCreated();
}

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
