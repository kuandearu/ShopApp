using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.BannerDetail;
using api.Models;

namespace api.Mappers
{
    public static class BannerDetailMappers
    {
        public static BannerDetailDTO ToBannerDetailDTO(this BannerDetail bannerDetail){
            return new BannerDetailDTO {
                Id = bannerDetail.Id,
                ProductId = bannerDetail.ProductId,
                BannerId = bannerDetail.BannerId,
                CreatedAt = bannerDetail.CreatedAt,
                UpdatedAt = bannerDetail.UpdatedAt
            };
        }

        public static BannerDetail ToCreateBannerDetailResponseDTO(this CreateBannerDetailResponseDTO dTO){
            return new BannerDetail{
                ProductId = dTO.ProductId,
                BannerId = dTO.BannerId,
                CreatedAt = DateTime.UtcNow            
            };
        }

        public static BannerDetail ToUpdateBannerDetailResponseDTO(this UpdateBannerDetailResponseDTO dTO){
            return new BannerDetail{
                ProductId = dTO.ProductId,
                BannerId = dTO.BannerId,
                UpdatedAt = DateTime.UtcNow            
            };
        }
    }
}