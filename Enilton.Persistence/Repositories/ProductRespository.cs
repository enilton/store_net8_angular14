using Enilton.Domain.Entities;
using Enilton.Domain.Interfaces;
using Enilton.Persistence.Context;
using MongoDB.Driver;

namespace Enilton.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MongoDbContext _context;

        public ProductRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            await _context.Products.InsertOneAsync(product);
        }

        public async Task UpdateAsync(Product product)
        {
            await _context.Products.ReplaceOneAsync(p => p.Id == product.Id, product);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Products.DeleteOneAsync(p => p.Id == id);
        }
    }
}
