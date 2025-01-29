using Enilton.Application.DTOs.Customer;
using MediatR;

namespace Enilton.Application.Queries.Customer
{
    public class GetAllCustomersQuery : IRequest<IEnumerable<CustomerDTO>>
    {

    }
}
