

/*
Deacription :

使用statup filters 功能拓展Startup
    使用 IStartupFilter:
        1、为了在应用程的中间件管道开始或结束不显示调用Use{Middleware}配置中间件, 使用IStartupFilter将默认值添加到管道的开头，而无需显式注册默认中间件,
        IStartupFilter允许不同组件来代表作者调用Use{Middleware}
        2、为了创建管道的Configure方法, IStartupFilter.Configure可以设置一个中间件在库添加到中间件库的前或者后运行
    IStartupFilter 有Configure方法, 接受并返回一个Action<IApplicationBuilder>, IApplicationBuilder定义了一个类配置应用程序处理管道    
    每个IStartupFilter能够添加一个或者多个中间件在请求管道, 按照他们添加到容器中的顺序被调用, Filters可以在将控制权传递给下一个过滤器之前或之后添加中间件, 
    因此他们也可以附加到应用程序管道的开始或者结束位置
    
    下面例子演示如何使用IStartupFilter注册一个中间件
    
    RequestSetOptionsMiddleware中间件从查询字符串参数设置一个options的值


IStartupFilter注册的顺序决定了中间件的执行顺序:

如果多个类IStartupFilter内部可以使用相同的对象进行交互, 如果顺序是重要的, IStartupFilter服务注册的顺序必须匹配他们中间件运行的顺序

库可以添加具有一个或多个IStartupFilter的实现, 注册的IStartupFilter接口可以运行在应用程序另外一些中间件的前面或者后面 
.若要在库的 IStartupFilter 添加的中间件之前调用IStartupFilter的中间件，请执行以下操作：

在库添加到服务容器之前定位服务注册

要在此后调用，请在添加库之后定位服务注册

*/

using Microsoft.AspNetCore.Http;

namespace AspNetCore.Startup.Middleware
{
    public class RequestSetOptionsStartupFilter : IStartupFilter
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {

            return builder =>
            {
                builder.UseMiddleware<RequestSetOptionsMiddleware>();
                //next 表示应用程序中间件
                next(builder);
            };
        }
    }

}
