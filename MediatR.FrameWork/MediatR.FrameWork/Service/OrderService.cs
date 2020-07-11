using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR.FrameWork.Practice.Service
{
    public class OrderService : IOrderService
    {
        public async Task<List<Order>> GetOrders()
        {
            var orders =await CreateOrders();
            return orders;
        }

        public async Task<Order> GetSingleOrder(int orderId)
        {
            var orders = await CreateOrders();
            var singleOrder =orders.Where(r=>r.OrderId==orderId).FirstOrDefault();
            return singleOrder;
        }
        public async Task<Order> CreateOrder(Order order)
        {
            var orders = await CreateOrders();
            orders.Add(order);
            return order;
        }
        private async Task<List<Order>> CreateOrders()
        {
            var orders = new List<Order>()
            {
                new Order{ OrderId=1, Name="Coffe", Price=30, Quantity=1, CreateTime=DateTimeOffset.Now },
                new Order{ OrderId=2, Name="Hamburger ", Price=40, Quantity=1, CreateTime=DateTimeOffset.Now },
                new Order{ OrderId=3, Name="Cake", Price=50, Quantity=2, CreateTime=DateTimeOffset.Now },
                new Order{ OrderId=4, Name="Milk", Price=60, Quantity=3, CreateTime=DateTimeOffset.Now }
            };
            return orders;
        }
    }



}
