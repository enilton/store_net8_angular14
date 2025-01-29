using Enilton.Application.DTOs.Customer;
using MediatR;

namespace Enilton.Application.Commands.Customer
{
    public class CreateCustomerCommand : IRequest<CustomerDTO>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
