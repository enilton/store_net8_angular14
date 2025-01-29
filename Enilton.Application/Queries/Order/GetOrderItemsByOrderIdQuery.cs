using Enilton.Application.DTOs.Order;
using MediatR;

namespace Enilton.Application.Queries.Order
{
    public class GetOrderItemsByOrderIdQuery : IRequest<IEnumerable<OrderItemDTO>>
    {
        public Guid OrderId { get; set; }
    }
}
