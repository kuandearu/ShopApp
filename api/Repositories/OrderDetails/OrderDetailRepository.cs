using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.OrderDetails
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ShopAppDbContext _context;

        public OrderDetailRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail?> GetOrderDetailByIdAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail)
        {
            _context.OrderDetails.Add(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }

        public async Task<OrderDetail?> UpdateOrderDetailAsync(OrderDetail orderDetail, int id)
        {
            var existingOrderDetail = await _context.OrderDetails.FindAsync(id);
            if (existingOrderDetail == null)
                return null;

            existingOrderDetail.OrderId = orderDetail.OrderId;
            existingOrderDetail.ProductId = orderDetail.ProductId;
            existingOrderDetail.Price = orderDetail.Price;
            existingOrderDetail.Quantity = orderDetail.Quantity;
            existingOrderDetail.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingOrderDetail;
        }

        public async Task<OrderDetail?> DeleteOrderDetailAsync(int id)
        {
            var orderDetail = await _context.OrderDetails.FindAsync(id);
            if (orderDetail == null)
                return null;

            _context.OrderDetails.Remove(orderDetail);
            await _context.SaveChangesAsync();
            return orderDetail;
        }
    }
}