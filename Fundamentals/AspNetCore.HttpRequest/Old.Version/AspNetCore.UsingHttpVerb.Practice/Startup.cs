using AspNetCore.UsingHttpVerb.Practice.Handlers;
using AspNetCore.UsingHttpVerb.Practice.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace AspNetCore.UsingHttpVerb.Practice
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(options =>
                options.UseInMemoryDatabase("TodoItems"));
            services.AddHttpContextAccessor();
            services.AddHttpClient<TodoClient>((serviceProvider, httpClient) =>
            {
                var httpRequest = serviceProvider.GetRequiredService<IHttpContextAccessor>().HttpContext.Request;
                httpClient.BaseAddress = new Uri(UriHelper.BuildAbsolute(
                    httpRequest.Scheme, httpRequest.Host, httpRequest.PathBase));
                httpClient.Timeout = TimeSpan.FromSeconds(5);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (httpRequestMessage, cert, certChain, policyErrors) =>
                {
                    return true;
                }
            }
            );
            services.AddScoped<IOperationScoped, OperationScoped>();

            services.AddTransient<OperationHandler>();
            services.AddTransient<OperationResponseHandler>();

            services.AddHttpClient("operation")
                .AddHttpMessageHandler<OperationHandler>()
                .AddHttpMessageHandler<OperationResponseHandler>()
                .SetHandlerLifetime(TimeSpan.FromSeconds(50));

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
