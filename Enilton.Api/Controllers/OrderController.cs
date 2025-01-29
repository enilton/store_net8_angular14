using Enilton.Application.Commands.Order;
using Enilton.Application.Queries.Order;
using Enilton.Application.Requests.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Enilton.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            var query = new GetOrderByIdQuery { Id = id };
            var order = await _mediator.Send(query);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var query = new GetAllOrdersQuery();
            var orders = await _mediator.Send(query);
            return Ok(orders);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (request == null || request.Items == null || !request.Items.Any())
            {
                return BadRequest(new { error = "Order and items are required." });
            }

            var command = new CreateOrderCommand
            {
                CustomerId = request.CustomerId,
                OrderDate = request.OrderDate,
                Status = request.Status,
                TotalAmount = request.TotalAmount,
                Items = request.Items
            };

            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var command = new DeleteOrderCommand { Id = id };
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> CalculateOrder([FromBody] CalculateOrderRequest request)
        {
            var command = new CalculateOrderCommand { Request = request };
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpGet("{orderId:guid}/items")]
        public async Task<IActionResult> GetOrderItemsByOrderId(Guid orderId)
        {
            var query = new GetOrderItemsByOrderIdQuery { OrderId = orderId };
            var result = await _mediator.Send(query);

            if (result == null || !result.Any())
            {
                return NotFound($"No items found for OrderId {orderId}");
            }

            return Ok(result);
        }
    }
}
