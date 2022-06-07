using AspNetCore.ChangeToToken.Extensions;
using AspNetCore.ChangeToToken.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static AspNetCore.ChangeToToken.Utilities.Utilities;

namespace AspNetCore.ChangeToToken
{
    public class Startup
    {
        private byte[] _appsettingsHash = new byte[20];
        private byte[] _appsettingsEnvHash = new byte[20];

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Method 2 ConfigurationMonitor
            services.AddSingleton<IConfigurationMonitor,ConfigurationMonitor>();
            #endregion
            #region Method 3 FileStream 监控文件是否变更,如果变更通知缓存失效
            services.AddMemoryCache();
            services.AddSingleton<FileService>();
            #endregion
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            #region Method 1 监控应用程序配置文件变动
            ChangeToken.OnChange(
                () => Configuration.GetReloadToken(), (env) => InvokeChange(env), env);
            #endregion


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
        public void InvokeChange(IWebHostEnvironment webHostEnvironment)
        {
            var appSettingsHash = ComputeHash("appSettings.json");
            var appSettingsEnvHash = ComputeHash($"appSettings.{webHostEnvironment.EnvironmentName}.json");

            if (!_appsettingsEnvHash.SequenceEqual(appSettingsEnvHash)
               || !_appsettingsHash.SequenceEqual(appSettingsHash))
            {
                _appsettingsEnvHash = appSettingsEnvHash;
                _appsettingsHash = appSettingsHash;
                WriteConsole("Configuration changed (Simple Startup Change Token)");
            }
        }
    }
}
