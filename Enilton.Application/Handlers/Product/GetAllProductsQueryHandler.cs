using Enilton.Application.DTOs.Product;
using Enilton.Application.Queries.Product;
using Enilton.Persistence.Context;
using MediatR;
using MongoDB.Driver;

namespace Enilton.Application.Handlers.Product
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDTO>>
    {
        private readonly MongoDbContext _context;

        public GetAllProductsQueryHandler(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _context.Products.Find(_ => true).ToListAsync(cancellationToken);

            return products.Select(p => new ProductDTO
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price
            });
        }
    }
}
