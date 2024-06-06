using MediatR.FrameWork.Practice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.FrameWork.Practice.Handlers
{
    public class CreateOrderHandler : IRequestHandler<CreateOrderCommand, Order>
    {
        private readonly IOrderService _orderService;
        public CreateOrderHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<Order> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = new Order()
            {
                OrderId = command.OrderId,
                Name = command.Name,
                Price = 200,
                Quantity = 1,
                CreateTime = DateTimeOffset.Now
            };
            var newOrder = await _orderService.CreateOrder(order);
            return newOrder;
        }
    }
}
