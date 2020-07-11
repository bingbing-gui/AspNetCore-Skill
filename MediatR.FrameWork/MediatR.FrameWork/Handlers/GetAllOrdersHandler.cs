using MediatR.FrameWork.Practice.Queries;
using MediatR.FrameWork.Practice.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatR.FrameWork.Practice.Handlers
{
    public class GetAllOrdersHandler : IRequestHandler<GetAllOrderQuery, List<Order>>
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<GetAllOrdersHandler> _logger;
        public GetAllOrdersHandler(IOrderService orderService, ILogger<GetAllOrdersHandler> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        public async Task<List<Order>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
        {
            var orders =await _orderService.GetOrders();
            return orders;
        }

    }
}
