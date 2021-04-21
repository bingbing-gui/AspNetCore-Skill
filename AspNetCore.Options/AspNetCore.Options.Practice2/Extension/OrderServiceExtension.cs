using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Options.Project.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Options.Practice2.Extension
{
    public static class OrderServiceExtension
    {

        public static IServiceCollection AddOrderService(this IServiceCollection services, IConfiguration configuration)
        {
            //配置IOptions 对象
            services.Configure<OrderServiceOptions>(configuration);//Configuration.GetSection("OrderServiceOptions"));
            //将值加载到内存做一些特殊处理
            services.PostConfigure<OrderServiceOptions>(option =>
            {
                option.MaxOrderCount += 100;
            });
            services.AddScoped<IOrderService, OrderService>();
            return services;
        }

    }
}
