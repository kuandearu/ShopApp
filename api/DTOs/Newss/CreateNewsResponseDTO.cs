using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Newss
{
    public class CreateNewsResponseDTO
    {
        
        [StringLength(200, ErrorMessage = "Title can't be longer than 200 characters.")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "Image URL can't be longer than 500 characters.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Invalid image URL format.")]
        public string? Image { get; set; }

        [Required(ErrorMessage = "Content is required.")]
        [MinLength(50, ErrorMessage = "Content must be at least 50 characters long.")]
        public string Content { get; set; }
    }
}