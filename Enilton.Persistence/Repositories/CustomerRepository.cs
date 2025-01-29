using Enilton.Domain.Entities;
using Enilton.Domain.Interfaces;
using Enilton.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Enilton.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MongoDbContext _context;

        public CustomerRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers.Find(c => c.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers.Find(_ => true).ToListAsync();
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.InsertOneAsync(customer);
        }

        public async Task UpdateAsync(Customer customer)
        {
            await _context.Customers.ReplaceOneAsync(c => c.Id == customer.Id, customer);
        }

        public async Task DeleteAsync(Guid id)
        {
            await _context.Customers.DeleteOneAsync(c => c.Id == id);
        }
    }
}
