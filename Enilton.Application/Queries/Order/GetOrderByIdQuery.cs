using Enilton.Application.DTOs.Order;
using MediatR;

namespace Enilton.Application.Queries.Order
{
    public class GetOrderByIdQuery : IRequest<OrderDTO>
    {
        public Guid Id { get; set; }
    }
}
