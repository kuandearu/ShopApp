using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Order;
using api.Enums;
using api.Models;

namespace api.Mappers
{
    public static class OrderMappers
    {
        public static OrderDTO ToOrderDTO(this Order order){
            return new OrderDTO {
                Id = order.Id,
                UserId = order.UserId,
                Status = order.Status,
                Note = order.Note,
                CreatedAt = order.CreatedAt,
                UpdatedAt = order.UpdatedAt,
                OrderDetails = order.OrderDetails?.Select(od => od.ToOrderDetailDTO()).ToList(),
            };
        }

        public static Order ToCreateOrderResponseDTO(this CreateOrderResponseDTO dTO){
            return new Order {
                UserId = dTO.UserId,
                Status = OrderStatus.Pending,
                Note = dTO.Note,
                Total = 0,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Order ToUpdateOrderResponseDTO(this UpdateOrderResponseDTO dTO){
            return new Order {
                Status = dTO.Status,
                Note = dTO.Note,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}