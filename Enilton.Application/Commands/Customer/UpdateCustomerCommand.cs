using Enilton.Application.DTOs.Customer;
using MediatR;

namespace Enilton.Application.Commands.Customer
{
    public class UpdateCustomerCommand : IRequest<CustomerDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
