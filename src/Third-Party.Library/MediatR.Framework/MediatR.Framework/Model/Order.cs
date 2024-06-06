using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatR.FrameWork.Practice
{
    public class Order
    {
        public int OrderId { get; set; }

        public string Name { get; set; }

        public decimal Quantity { get; set; }

        public Decimal Price { get; set; }

        public DateTimeOffset CreateTime { get; set; }

    }
}
