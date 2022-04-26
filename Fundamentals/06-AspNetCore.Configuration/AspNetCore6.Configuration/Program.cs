#define KeyPerFileConfigurationProvider
#if Scene
#region
using AspNetCore6.Configuration.Options;
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostBuilderContext, config) =>
{
    config.AddJsonFile("Array.json", optional: true, reloadOnChange: true);
    config.AddJsonFile("Subsection.json", optional: true, reloadOnChange: true);
});
// Add services to the container.
builder.Services.AddRazorPages();
//ConfigurationOption => IOptions
builder.Services.Configure<PositionOptions>(
    builder.Configuration.GetSection(PositionOptions.Position));
var app = builder.Build();
var config = app.Services.GetRequiredService<IConfiguration>();
foreach (var c in config.AsEnumerable())
{
    Console.WriteLine(c.Key + " = " + c.Value);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion
#elif SwitchMappings
#region
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
var switchMappings = new Dictionary<string, string>()
         {
             { "-k1", "key1" },
             { "-k2", "key2" },
             { "--alt3", "key3" },
             { "--alt4", "key4" },
             { "--alt5", "key5" },
             { "--alt6", "key6" },
         };
builder.Configuration.AddCommandLine(args, switchMappings);
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion

#elif IniConfigurationProvider 
#region
using AspNetCore6.Configuration.Options;
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
    {
        configurationBuilder.Sources.Clear();
        var environment = hostBuilderContext.HostingEnvironment;
        configurationBuilder.AddIniFile("appsettings.ini", optional: true, reloadOnChange: true);
        configurationBuilder.AddIniFile($"appsettings.{environment.EnvironmentName}.ini", optional: true, reloadOnChange: true);

        configurationBuilder.AddEnvironmentVariables();
        if (args != null)
        {
            configurationBuilder.AddCommandLine(args);
        }
    }
);
// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion

#elif JsonConfigurationProvider
#region
using AspNetCore6.Configuration.Options;
var builder = WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
    {
        configurationBuilder.Sources.Clear();
        var environment = hostBuilderContext.HostingEnvironment;
        configurationBuilder.AddXmlFile("appsettings.xml", optional: true, reloadOnChange: true);
        configurationBuilder.AddXmlFile($"appsettings.{environment.EnvironmentName}.xml", optional: true, reloadOnChange: true);
        configurationBuilder.AddEnvironmentVariables();
        if (args != null)
        {
            configurationBuilder.AddCommandLine(args);
        }
    }
);
// Add services to the container.
builder.Services.AddRazorPages();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endregion
#elif MemoryConfigurationProvider
var builder=WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) => 
{
    configurationBuilder.Sources.Clear();

    var dict = new Dictionary<string, string>
        {
           {"MyKey", "Dictionary MyKey Value"},
           {"Position:Title", "Dictionary_Title"},
           {"Position:Name", "Dictionary_Name" },
           {"Logging:LogLevel:Default", "Warning"}
        };

    configurationBuilder.AddInMemoryCollection(dict);

    configurationBuilder.AddEnvironmentVariables();

    if (args != null)
    {
        configurationBuilder.AddCommandLine(args);
    }
});
builder.Services.AddRazorPages();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();

#elif KeyPerFileConfigurationProvider 
var builder =WebApplication.CreateBuilder(args);
builder.Host.ConfigureAppConfiguration((hostBuilderContext, configurationBuilder) =>
{
    var path = Path.Combine(Directory.GetCurrentDirectory(),"path/to/files");
    configurationBuilder.AddKeyPerFile(path, optional:true);
});
builder.Services.AddRazorPages();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapRazorPages();
app.Run();
#endif
