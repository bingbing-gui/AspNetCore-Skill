using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http;

namespace AspNetCore.Route
{
    public class MiddlewareFlowStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Location 1: before routing runs, endpoint is always null here
            app.Use(next => context =>
            {
                Console.WriteLine($"1. Endpoint:{context.GetEndpoint()?.DisplayName ?? "(null)"}");
                return next(context);
            });
            app.UseRouting();
            // Location 2: after routing runs, endpoint will be non-null if routing found a match
            app.Use(next => context =>
              {
                  Console.WriteLine($"2.Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
                  return next(context);
              });
            app.UseEndpoints(endpoints =>
            {
                // Location 3: runs when this endpoint matches
                endpoints.MapGet("/", context =>
                {
                    Console.WriteLine(
                       $"3. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
                    return Task.CompletedTask;
                }).WithDisplayName("Hello");

            });
            app.Use(next => context =>
            {
                //Location 4: runs after UseEndpoints - will only run if there was no match
                Console.WriteLine($"4. Endpoint: {context.GetEndpoint()?.DisplayName ?? "(null)"}");
                return next(context);
            });
        }
    }
}
