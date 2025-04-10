using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Banner;
using api.Models;

namespace api.Mappers
{
    public static class BannerMappers
    {
        public static BannerDTO ToBannerDTO(this Banner banner){
            return new BannerDTO{
                Id = banner.Id,
                Name = banner.Name,
                Image = banner.Image,
                Status = banner.Status,
                CreatedAt = banner.CreatedAt,
                UpdatedAt = banner.UpdatedAt,
                BannerDetails = banner.BannerDetails?.Select(b => b.ToBannerDetailDTO()).ToList(),

            };
        }

        public static Banner ToCreateBannerResponseDTO(this CreateBannerResponseDTO dTO){
            return new Banner{
                Name = dTO.Name,
                Image = dTO.Image,
                Status = dTO.Status,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static Banner ToUpdateBannerResponseDTO(this UpdateBannerResponseDTO dTO){
            return new Banner{
                Name = dTO.Name,
                Image = dTO.Image,
                Status = dTO.Status,
                UpdatedAt = DateTime.UtcNow
            };
        }
    }
}