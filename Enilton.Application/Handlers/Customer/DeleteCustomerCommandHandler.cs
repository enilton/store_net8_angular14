using Enilton.Application.Commands.Customer;
using Enilton.Persistence.Context;
using MediatR;
using MongoDB.Driver;

namespace Enilton.Application.Handlers.Customer
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, Unit>
    {
        private readonly MongoDbContext _context;

        public DeleteCustomerCommandHandler(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var result = await _context.Customers.DeleteOneAsync(
                c => c.Id == request.Id,
                cancellationToken);

            if (result.DeletedCount == 0)
                throw new KeyNotFoundException($"Customer with ID {request.Id} not found.");

            return Unit.Value;
        }
    }
}
