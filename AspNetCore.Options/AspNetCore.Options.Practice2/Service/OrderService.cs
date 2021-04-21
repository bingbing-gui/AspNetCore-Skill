using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Options.Project.Service
{
    public interface IOrderService
    {
        int ShowIOptionsMaxOrderCount();

        int ShowIOptionsSnapshotMaxOrderCount();

        int ShowIOptionsMonitorOrderCount();
    }

    public class OrderService : IOrderService
    {
        IOptions<OrderServiceOptions> _options;
        IOptionsSnapshot<OrderServiceOptions> _optionsSnapshot;
        IOptionsMonitor<OrderServiceOptions> _optionsMonitor;
        public OrderService(IOptions<OrderServiceOptions> options, IOptionsSnapshot<OrderServiceOptions> optionsSnapshot, IOptionsMonitor<OrderServiceOptions> optionsMonitor)
        {
            _options = options;
            _optionsSnapshot = optionsSnapshot;
            _optionsMonitor = optionsMonitor;
            _optionsMonitor.OnChange<OrderServiceOptions>(option =>
            {
                Console.WriteLine($"IOptionsMonitor OnChange MaxCount={option.MaxOrderCount}");
            });
        }
        public int ShowIOptionsMaxOrderCount()
        {
            return _options.Value.MaxOrderCount;
        }
        /// <summary>
        /// 单例模式
        /// </summary>
        /// <returns></returns>
        public int ShowIOptionsMonitorOrderCount()
        {
            return _optionsMonitor.CurrentValue.MaxOrderCount;
        }
        /// <summary>
        /// scope 模式
        /// </summary>
        /// <returns></returns>
        public int ShowIOptionsSnapshotMaxOrderCount()
        {
            return _optionsSnapshot.Value.MaxOrderCount;
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
