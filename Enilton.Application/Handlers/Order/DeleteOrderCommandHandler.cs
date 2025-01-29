using Enilton.Application.Commands.Order;
using Enilton.Persistence.Context;
using MediatR;

namespace Enilton.Application.Handlers.Order
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly SqlServerDbContext _context;

        public DeleteOrderCommandHandler(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _context.Orders.FindAsync(new object[] { request.Id }, cancellationToken);

            if (order == null) throw new KeyNotFoundException($"Order with ID {request.Id} not found.");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
