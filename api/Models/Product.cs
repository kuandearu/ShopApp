using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Product
    {
        [Key] 
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive real number.")]
        public double Price { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Old price must be a positive real number.")]
        public double OldPrice { get; set; }

        [StringLength(500, ErrorMessage = "Image URL can't be longer than 500 characters.")]
        [DataType(DataType.ImageUrl, ErrorMessage = "Invalid image URL format.")]
        public string Image { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [StringLength(1000, ErrorMessage = "Specification cannot exceed 1000 characters.")]
        public string Specification { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Buy turn must be a positive integer.")]
        public int BuyTurn { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be a positive integer.")]
        public int Quantity { get; set; }

        [Required]
        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        [Required]
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        public ICollection<Feedback>? Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<NewsDetail>? NewsDetails {get; set;} = new List<NewsDetail>();
        public ICollection<BannerDetail>? BannerDetails {get; set;} = new List<BannerDetail>();
        
    }
}
