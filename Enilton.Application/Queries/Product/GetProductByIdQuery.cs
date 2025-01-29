using Enilton.Application.DTOs.Product;
using MediatR;

namespace Enilton.Application.Queries.Product
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public Guid Id { get; set; }
    }
}
