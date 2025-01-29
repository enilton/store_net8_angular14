using Enilton.Application.DTOs.Order;
using Enilton.Application.Queries.Order;
using Enilton.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enilton.Application.Handlers.Order
{
    public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO>
    {
        private readonly SqlServerDbContext _context;

        public GetOrderByIdQueryHandler(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (order == null) return null;

            return new OrderDTO
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status.ToString()
            };
        }
    }
}
