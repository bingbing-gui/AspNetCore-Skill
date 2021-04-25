using AspNetCore.ExceptionHandle.Exception;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.ExceptionHandle
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

           // services.AddControllers();
            services.AddControllersWithViews();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AspNetCore.ExceptionHandle", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            /*启用异常处理页,当APP运行在开发环境中
              异常处理中间件放到管道的早期有助于捕获后面中间
              的异常. 只有app 在开发环境中运行时，才能捕获开发环境中的异常
              当在生产环境中运行时，详细的信息都不显示.
             开发环境中的异常页包含了下面异常信息:
             1. 异常堆栈
             2. 查询字符串参数
             3. cookies 信息
             4. Http 请求的头部信息
            */
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AspNetCore.ExceptionHandle v1"));
            }
            else
            {
                /*
                 针对生产环境配置一个异常处理的错误页
                 1.能捕获并记录未处理的异常
                 2.使用指定的路径在管道中重新执行一个请求,
                 Re-executes the request in an alternate pipeline using the path indicated. 
                 The request isn't re-executed if the response has started. 
                 The template generated code re-executes the request using the /Error path
                 在下面code 中：
                 UseExceptionHandler 方法在添加异常处理中间件在非开发环境中.

                 */
                //app.UseExceptionHandler("/error");
                /*
                 异常处理Lambda表达式
                 UseExceptionHandler方法可以通过lambda 已定义异常,
               */
            }
            app.UseExceptionHandler("/error");
            //app.UseExceptionHandler(errorApp =>
            //{
            //    errorApp.Run(async context =>
            //    {
            //        var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            //        var ex = exceptionHandlerPathFeature?.Error;
            //        var knowException = ex as IKnowException;
            //        if (knowException == null)
            //        {
            //            var logger = context.RequestServices.GetService<ILogger<MyExceptionFilterAttribute>>();
            //            logger.LogError(ex, ex.Message);
            //            knowException = KnowException.Unknown;
            //        }
            //        else
            //        {
            //            knowException = KnowException.FromKnownException(knowException);
            //        }
            //        var jsonOptions = context.RequestServices.GetService<IOptions<JsonOptions>>();
            //        context.Response.ContentType = "application/json;charset=utf-8";
            //        await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(knowException,jsonOptions.Value.JsonSerializerOptions));
            //    });
            //});
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
