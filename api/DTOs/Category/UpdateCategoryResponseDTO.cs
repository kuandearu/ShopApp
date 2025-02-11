using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Category
{
    public class UpdateCategoryResponseDTO
    {
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Image URL can't be longer than 500 characters.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Invalid image URL format.")]
        public string Image { get; set; }
    }
}