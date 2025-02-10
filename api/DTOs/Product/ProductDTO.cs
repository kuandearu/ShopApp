using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public double OldPrice { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Specification { get; set; }
        public int BuyTurn { get; set; }
        public int Quantity { get; set; }

        [ForeignKey(nameof(Brand))]
        public int BrandId { get; set; }

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
        // public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
        // public ICollection<Feedback>? Feedbacks { get; set; } = new List<Feedback>();
        // public ICollection<NewsDetail>? NewsDetails {get; set;} = new List<NewsDetail>();
        // public ICollection<BannerDetail>? BannerDetails {get; set;} = new List<BannerDetail>();
    }
}