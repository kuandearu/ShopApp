using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.Models
{
    public class Banner
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Banner name cannot exceed 100 characters.")]
        public string Name { get; set; }
        [Required]
        [StringLength(500, ErrorMessage = "Image URL can't be longer than 500 characters.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Invalid image URL format.")]
        public string Image { get; set; }
        [Required]
        public BannerStatus Status { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [DataType(DataType.DateTime)] 
        public DateTime UpdatedAt { get; set; }
        public ICollection<BannerDetail>? BannerDetails {get; set;} = new List<BannerDetail>();
    }
}