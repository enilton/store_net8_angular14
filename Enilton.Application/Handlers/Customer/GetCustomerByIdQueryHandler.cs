using Enilton.Application.DTOs.Customer;
using Enilton.Application.Queries.Customer;
using Enilton.Persistence.Context;
using MediatR;
using MongoDB.Driver;

namespace Enilton.Application.Handlers.Customer
{
    public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, CustomerDTO>
    {
        private readonly MongoDbContext _context;

        public GetCustomerByIdQueryHandler(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<CustomerDTO> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            var customer = await _context.Customers
                .Find(c => c.Id == request.Id)
                .FirstOrDefaultAsync(cancellationToken);

            if (customer == null) return null;

            return new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone
            };
        }
    }
}
