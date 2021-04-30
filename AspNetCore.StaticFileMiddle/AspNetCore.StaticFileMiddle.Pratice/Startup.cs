using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.StaticFileMiddle.Pratice
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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCore.StaticFileMiddle.Pratice", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        const int BufferSize = 64 * 1024;
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCore.StaticFileMiddle.Pratice v1"));
            }

            app.UseHttpsRedirection();
            //启动静态文件路径浏览
            //app.UseDirectoryBrowser();
            //启用访问静态文件中间件
            app.UseStaticFiles();
            //使用自定义目录
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/staticfiles",
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "StaticFile"))
            });

            app.MapWhen(context =>
                {
                    return !context.Request.Path.Value.StartsWith("/api");
                },
                //重定向方式
                appBuilder =>
                {
                    var option = new RewriteOptions();
                    option.AddRewrite(".*", "/index.html", true);
                    appBuilder.UseStaticFiles();
                    //使用断路器的方式
                    //appBuilder.Run(async c =>
                    //{
                    //    var file = env.WebRootFileProvider.GetFileInfo("index.html");
                    //    c.Response.ContentType = "text/html";
                    //    using (var fileStream = new FileStream(file.PhysicalPath, FileMode.Open, FileAccess.Read))
                    //    {
                    //        await StreamCopyOperation.CopyToAsync(fileStream, c.Response.Body, null,BufferSize, c.RequestAborted);
                    //    }
                    //});
                }
                );
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
