using MediatR;

namespace Enilton.Application.Commands.Order
{
    public class DeleteOrderCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
