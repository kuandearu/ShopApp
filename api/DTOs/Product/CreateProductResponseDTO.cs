using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Product
{
    public class CreateProductResponseDTO
    {
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

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

    }
}