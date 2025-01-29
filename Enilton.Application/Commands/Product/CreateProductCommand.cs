using Enilton.Application.DTOs.Product;
using MediatR;

namespace Enilton.Application.Commands.Product
{
    public class CreateProductCommand : IRequest<ProductDTO>
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
