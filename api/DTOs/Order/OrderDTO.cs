using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.OrderDetail;
using api.Enums;

namespace api.DTOs.Order
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderStatus Status { get; set; }

        public string? Note { get; set; }

        public decimal Total => OrderDetails?.Sum(od => od.Price * od.Quantity) ?? 0;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<OrderDetailDTO>? OrderDetails { get; set; } = new List<OrderDetailDTO>();
    }
}