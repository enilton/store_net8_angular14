using MediatR;

namespace Enilton.Application.Commands.Product
{
    public class DeleteProductCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
