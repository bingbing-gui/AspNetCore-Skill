using AspNetCore.Middleware.Controllers;
using AspNetCore.Middleware.MyMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Middleware
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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCore.Middleware", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCore.Middleware v1"));
            }

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("use hello world");
            //    await next();
            //});

            //app.Map("/map", mapBuilder =>
            //{
            //    //Use 可以带下一个中间件参数
            //    mapBuilder.Use(async (context, next) =>
            //    {
            //        await next();
            //        await context.Response.WriteAsync("map hello world");
            //    });
            //});

            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.Keys.Contains("flag");
            //}, builder =>
            //{
            //    //run 方法只能是末尾中间件
            //    builder.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("flag");
            //    });
            //});

            app.UseMyMiddleware();


            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.Use(async (context, next) =>
            {
                //if (context.GetEndpoint()?.Metadata.GetMetadata<RequiresAuditAttribute>() is not null)
                //{
                //    Console.WriteLine($"ACCESS TO SENSITIVE DATA AT: {DateTime.UtcNow}");
                //}

                await next();
            });
            
            //app.MapGet("/", () => "Audit isn't required.");
            //app.MapGet("/sensitive", () => "Audit required for sensitive data.")
            //    .WithMetadata(new RequiresAuditAttribute());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", () => "Audit isn't required.");
                endpoints.MapGet("/sensitive", () => "Audit required for sensitive data.")
                    .WithMetadata(new RequiresAuditAttribute());
                endpoints.MapControllers();
            });
        }
    }
}
