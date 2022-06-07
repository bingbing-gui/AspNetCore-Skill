using AspNetCore.UsingHttpVerb.Practice.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.UsingHttpVerb.Practice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host=CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<TodoContext>();
                SeedContext(context);
                context.SaveChanges();
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        private static void SeedContext(TodoContext context)
        {
            context.TodoItems.Add(new TodoItem { Name = "Task #1", IsComplete = true });
            context.TodoItems.Add(new TodoItem { Name = "Task #2" });
            context.TodoItems.Add(new TodoItem { Name = "Task #3" });
            context.TodoItems.Add(new TodoItem { Name = "Task #4", IsComplete = true });
        }
    }
}
