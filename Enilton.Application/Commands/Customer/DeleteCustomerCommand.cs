using MediatR;

namespace Enilton.Application.Commands.Customer
{
    public class DeleteCustomerCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
    }
}
