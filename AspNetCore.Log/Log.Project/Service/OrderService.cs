using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Log.Project.Service
{
    public class OrderService : IOrderService
    {
        /// <summary>
        /// 通过强类型，泛型方式获取日志对象.
        /// </summary>
        ILogger<OrderService> _logger;
        public OrderService(ILogger<OrderService> logger)
        {
            _logger = logger;
        }
        public void WriteLog(string message)
        {
            //通过占位符替换.(在Log 输出的时候才拼接)
            _logger.LogInformation("OrderService {message}", message);
            //拼接好之后传给方法.
            _logger.LogInformation($"OrderService {message}");
        }
    }
}
