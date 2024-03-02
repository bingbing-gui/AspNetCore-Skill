using MediatR.FrameWork.Practice.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediatR.FrameWork.Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
        {
            var result = await _mediator.Send(command);
            //return CreatedAtAction("GetOrder", new { orderId = result.OrderId }, result);
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrderQuery();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetSingleOrder(int orderId)
        {
            var querySingle = new GetOrderByIdQuery(orderId);
            var order = await _mediator.Send(querySingle);
            if (order == null) return NotFound();
            else return Ok(order);
        }

    }
}