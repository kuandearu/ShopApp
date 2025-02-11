using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Brand;
using api.Models;

namespace api.Mappers
{
    public static class BrandMappers
    {
        public static BrandDTO ToBrandDTO(this Brand brand){
            return new BrandDTO {
                Id = brand.Id,
                Name = brand.Name,
                Image = brand.Image,
                Products = brand.Products?.Select(p => p.ToProductDTO()).ToList(),
                
            };
        }

        public static Brand ToCreateBrandResponse(this CreateBrandResponseDTO createBrandResponseDTO){
            return new Brand {
                Name = createBrandResponseDTO.Name,
                Image = createBrandResponseDTO.Image
            };
        }

        public static Brand ToUpdateBrandResponse(this UpdateBrandResponseDTO updateBrandResponseDTO){
            return new Brand {
                Name = updateBrandResponseDTO.Name,
                Image = updateBrandResponseDTO.Image
            };
        }
    }
}