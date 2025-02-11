using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Brand
{
    public class CreateBrandResponseDTO
    {

        [Required]
        [StringLength(100, ErrorMessage = "Brand name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Image URL can't be longer than 500 characters.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Invalid image URL format.")]
        public string Image { get; set; }
    }
}