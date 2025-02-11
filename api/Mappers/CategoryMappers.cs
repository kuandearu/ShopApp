using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Category;
using api.Models;

namespace api.Mappers
{
    public static class CategoryMappers
    {
        public static CategoryDTO ToCategoryDTO(this Category category){
            return new CategoryDTO {
                Id = category.Id,
                Name = category.Name,
                Image = category.Image,
                Products = category.Products?.Select(c => c.ToProductDTO()).ToList(),
                
            };
        }

        public static Category ToCreateCategoryResponse(this CreateCategoryResponseDTO dTO){
            return new Category {
                Name = dTO.Name,
                Image = dTO.Image
            };
        }

        public static Category ToUpdateCategoryResponse(this UpdateCategoryResponseDTO dTO){
            return new Category {
                Name = dTO.Name,
                Image = dTO.Image
            };
        }
    }
}