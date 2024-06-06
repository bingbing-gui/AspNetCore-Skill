using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR.FrameWork.Practice.Queries
{
    public class GetOrderByIdQuery : IRequest<Order>
    {
        public int OrderId 
        {
            get;
        }
        public GetOrderByIdQuery(int orderId)
        {
            OrderId = orderId;
        }
    }
}
