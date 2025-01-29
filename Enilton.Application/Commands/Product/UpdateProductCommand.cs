using Enilton.Application.DTOs.Product;
using MediatR;

namespace Enilton.Application.Commands.Product
{
    public class UpdateProductCommand : IRequest<ProductDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
