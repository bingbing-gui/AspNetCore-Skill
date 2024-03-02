using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR.FrameWork.Practice.Service
{
    public interface IOrderService
    {

        Task<List<Order>> GetOrders();

        Task<Order> GetSingleOrder(int orderId);

        Task<Order> CreateOrder(Order order);

    }
}
