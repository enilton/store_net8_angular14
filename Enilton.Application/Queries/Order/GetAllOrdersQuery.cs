using Enilton.Application.DTOs.Order;
using MediatR;

namespace Enilton.Application.Queries.Order
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDTO>>
    {

    }
}
