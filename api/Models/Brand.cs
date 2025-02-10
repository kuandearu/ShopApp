using System.ComponentModel.DataAnnotations;

namespace api.Models
{
    public class Brand
    {
        [Key] 
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Brand name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Image URL can't be longer than 500 characters.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Invalid image URL format.")]
        public string Image { get; set; }
        public ICollection<Product>? Products { get; set; } = new List<Product>();
    }
}
