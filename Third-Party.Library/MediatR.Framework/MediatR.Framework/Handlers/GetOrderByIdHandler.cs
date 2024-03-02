using MediatR.FrameWork.Practice.Queries;
using MediatR.FrameWork.Practice.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.FrameWork.Practice.Handlers
{
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, Order>
    {
        private readonly IOrderService _orderService;
        public GetOrderByIdHandler(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<Order> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderService.GetSingleOrder(request.OrderId);
            return order;
        }
    }
}
