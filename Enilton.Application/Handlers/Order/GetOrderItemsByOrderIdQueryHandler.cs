using Enilton.Application.DTOs.Order;
using Enilton.Application.Queries.Order;
using Enilton.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Enilton.Application.Handlers.Order
{
    public class GetOrderItemsByOrderIdQueryHandler : IRequestHandler<GetOrderItemsByOrderIdQuery, IEnumerable<OrderItemDTO>>
    {
        private readonly SqlServerDbContext _context;

        public GetOrderItemsByOrderIdQueryHandler(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItemDTO>> Handle(GetOrderItemsByOrderIdQuery request, CancellationToken cancellationToken)
        {
            var items = await _context.OrderItems.Where(i => i.OrderId == request.OrderId).ToListAsync(cancellationToken);

            return items.Select(i => new OrderItemDTO
            {
                Id = i.Id,
                OrderId = i.OrderId,
                ProductId = i.ProductId,
                ProductName = i.ProductName,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                TotalPrice = i.TotalPrice
            });
        }
    }
}
