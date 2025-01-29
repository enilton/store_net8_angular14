using Enilton.Application.DTOs.Product;
using MediatR;

namespace Enilton.Application.Queries.Product
{
    public class GetAllProductsQuery : IRequest<IEnumerable<ProductDTO>>
    {

    }
}
