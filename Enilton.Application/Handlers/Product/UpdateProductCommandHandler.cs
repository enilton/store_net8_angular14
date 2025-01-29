using Enilton.Application.Commands.Product;
using Enilton.Application.DTOs.Product;
using Enilton.Domain.Interfaces;
using MediatR;

namespace Enilton.Application.Handlers.Product
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDTO>
    {
        private readonly IProductRepository _productRepository;

        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<ProductDTO> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);
            if (product == null) throw new KeyNotFoundException("Product not found");

            product.Name = request.Name;
            product.Price = request.Price;

            await _productRepository.UpdateAsync(product);

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
        }
    }
}
