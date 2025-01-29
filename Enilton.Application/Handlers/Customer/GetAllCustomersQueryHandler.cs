using Enilton.Application.DTOs.Customer;
using Enilton.Application.Queries.Customer;
using Enilton.Persistence.Context;
using MediatR;
using MongoDB.Driver;

namespace Enilton.Application.Handlers.Customer
{
    public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, IEnumerable<CustomerDTO>>
    {
        private readonly MongoDbContext _context;

        public GetAllCustomersQueryHandler(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CustomerDTO>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            var customers = await _context.Customers.Find(_ => true).ToListAsync(cancellationToken);

            return customers.Select(c => new CustomerDTO
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email,
                Phone = c.Phone
            });
        }
    }
}
