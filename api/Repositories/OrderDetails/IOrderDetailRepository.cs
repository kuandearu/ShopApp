using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories.OrderDetails
{
    public interface IOrderDetailRepository
    {
        Task<IEnumerable<OrderDetail>> GetAllOrderDetailsAsync();
        Task<OrderDetail?> GetOrderDetailByIdAsync(int id);
        Task<OrderDetail> CreateOrderDetailAsync(OrderDetail orderDetail);
        Task<OrderDetail?> UpdateOrderDetailAsync(OrderDetail orderDetail, int id);
        Task<OrderDetail?> DeleteOrderDetailAsync(int id);
    }
}