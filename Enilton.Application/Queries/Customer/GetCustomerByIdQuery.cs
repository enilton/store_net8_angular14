using Enilton.Application.DTOs.Customer;
using MediatR;

namespace Enilton.Application.Queries.Customer
{
    public class GetCustomerByIdQuery : IRequest<CustomerDTO>
    {
        public Guid Id { get; set; }
    }
}
