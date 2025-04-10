using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Product;
using api.Models;

namespace api.Mappers
{
    public static class ProductMappers
    {
        public static ProductDTO ToProductDTO(this Product product){
            return new ProductDTO {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                OldPrice = product.OldPrice,
                Image = product.Image,
                Description = product.Description,
                Specification = product.Specification,
                BuyTurn = product.BuyTurn,
                Quantity = product.Quantity,
                BrandId = product.BrandId,
                CategoryId = product.CategoryId,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt,
                OrderDetails = product.OrderDetails?.Select(p => p.ToOrderDetailDTO()).ToList(),
                Feedbacks = product.Feedbacks?.Select(p => p.ToFeedbackDTO()).ToList(),
                NewsDetails = product.NewsDetails?.Select(p => p.ToNewsDetailDTO()).ToList(),
                BannerDetails = product.BannerDetails?.Select(p => p.ToBannerDetailDTO()).ToList(),

            };
        }

        public static Product ToCreateProductResponseDTO(this CreateProductResponseDTO dTO){
            return new Product {
                Name = dTO.Name,
                Price = dTO.Price,
                OldPrice = dTO.OldPrice,
                Image = dTO.Image,
                Description = dTO.Description,
                Specification = dTO.Specification,
                BuyTurn = dTO.BuyTurn,
                Quantity = dTO.Quantity,
                BrandId = dTO.BrandId,
                CategoryId = dTO.CategoryId,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public static Product ToUpdateProductResponseDTO(this UpdateProductResponseDTO dTO){
            return new Product {
                Name = dTO.Name,
                Price = dTO.Price,
                OldPrice = dTO.OldPrice,
                Image = dTO.Image,
                Description = dTO.Description,
                Specification = dTO.Specification,
                BuyTurn = dTO.BuyTurn,
                Quantity = dTO.Quantity,
                BrandId = dTO.BrandId,
                CategoryId = dTO.CategoryId,
                UpdatedAt = DateTime.UtcNow,
            };
        }
    }
}