using Enilton.Application.DTOs.Order;
using Enilton.Application.Queries.Order;
using Enilton.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enilton.Application.Handlers.Order
{
    public class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDTO>>
    {
        private readonly SqlServerDbContext _context;

        public GetAllOrdersQueryHandler(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.ToListAsync(cancellationToken);

            return orders.Select(o => new OrderDTO
            {
                Id = o.Id,
                CustomerId = o.CustomerId,
                OrderDate = o.OrderDate,
                TotalAmount = o.TotalAmount,
                Status = o.Status.ToString()
            });
        }
    }
}
