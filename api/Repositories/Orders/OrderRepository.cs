using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ShopAppDbContext _context;

        public OrderRepository(ShopAppDbContext context)
        {
            _context = context;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return null;
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderDetails).ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.OrderDetails).FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order?> UpdateOrderAsync(Order updatedOrder, int id)
        {
            var existingOrder = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
            if (existingOrder == null)
            {
                return null;
            }
            existingOrder.Status = updatedOrder.Status;
            existingOrder.Note = updatedOrder.Note;
            existingOrder.Total = updatedOrder.Total;
            existingOrder.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingOrder;
        }
    }
}