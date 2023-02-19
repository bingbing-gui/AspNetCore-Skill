using Identity.IdentityPolicy;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppIdentityDbContext>(
    options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"])
    );
builder.Services.AddIdentity<AppUser, IdentityRole>().
AddEntityFrameworkStores<AppIdentityDbContext>().
AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options =>
{
    #region 用户名和邮件
    options.User.RequireUniqueEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz";
    #endregion

    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;

});
#region 自定义密码策略
builder.Services.AddTransient<IPasswordValidator<AppUser>, CustomPasswordPolicy>();
#endregion

#region 自定义用户名和邮箱策略
builder.Services.AddTransient<IUserValidator<AppUser>, CustomUsernameEmailPolicy>();
#endregion

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
