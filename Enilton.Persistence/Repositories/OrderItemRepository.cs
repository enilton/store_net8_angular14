using Enilton.Domain.Entities;
using Enilton.Domain.Interfaces;
using Enilton.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

namespace Enilton.Persistence.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly SqlServerDbContext _context;

        public OrderItemRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetByIdAsync(Guid id)
        {
            return await _context.OrderItems.FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> GetByOrderIdAsync(Guid orderId)
        {
            return await _context.OrderItems.Where(oi => oi.OrderId == orderId).ToListAsync();
        }

        public async Task AddAsync(OrderItem orderItem)
        {
            await _context.OrderItems.AddAsync(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem orderItem)
        {
            _context.OrderItems.Update(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
        }
    }
}