﻿using Enilton.Domain.Entities;
using Enilton.Domain.Interfaces;
using Enilton.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Enilton.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly SqlServerDbContext _context;

        public OrderRepository(SqlServerDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task AddAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
