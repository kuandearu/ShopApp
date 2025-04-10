using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.OrderDetail;
using api.Models;

namespace api.Mappers
{
    public static class OrderDetailMappers
    {
        public static OrderDetailDTO ToOrderDetailDTO(this OrderDetail orderDetail){
            return new OrderDetailDTO {
                Id = orderDetail.Id,
                OrderId = orderDetail.OrderId,
                ProductId = orderDetail.ProductId,
                Price = orderDetail.Price,
                Quantity = orderDetail.Quantity,
                CreatedAt = orderDetail.CreatedAt,
                UpdatedAt = orderDetail.UpdatedAt
            };
        }

        public static OrderDetail ToCreateOrderDetailResponseDTO(this CreateOrderrDetailResponseDTO dTO, ShopAppDbContext context)
        {
            var product = context.Products.FirstOrDefault(p => p.Id == dTO.ProductId) ?? throw new Exception("Product not found");
            // if (product == null)
            // {
            //     throw new Exception("Product not found");
            // }
            return new OrderDetail
            {
                OrderId = dTO.OrderId,
                ProductId = dTO.ProductId,
                Price = product.Price, // Set OrderDetail price to Product price
                Quantity = dTO.Quantity,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static OrderDetail ToUpdateOrderDetailResponseDTO(this UpdateOrderrDetailResponseDTO dTO, ShopAppDbContext context){

            var product = context.Products.FirstOrDefault(p => p.Id == dTO.ProductId) ?? throw new Exception("Product not found");
            // if (product == null)
            // {
            //     throw new Exception("Product not found");
            // }
            return new OrderDetail{
                OrderId = dTO.OrderId,
                ProductId = dTO.ProductId,
                Price = product.Price, // Set OrderDetail price to Product price
                Quantity = dTO.Quantity,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}