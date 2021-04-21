using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Options.Project.Service
{
    public interface IOrderService
    {
        int ShowMaxOrderCount();

    }
    public class OrderService : IOrderService
    {
        OrderServiceOptions _options;
        public OrderService(OrderServiceOptions options)
        {
            _options = options;
        }

        public int ShowMaxOrderCount()
        {
            return _options.MaxOrderCount;
        }
    }
    /// <summary>
    /// 从配置里面读取值
    /// </summary>
    public class OrderServiceOptions
    {
        public int MaxOrderCount { get; set; } = 100;
    }
}
