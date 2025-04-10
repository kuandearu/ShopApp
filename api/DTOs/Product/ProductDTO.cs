using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.BannerDetail;
using api.DTOs.Feedback;
using api.DTOs.NewsDetail;
using api.DTOs.OrderDetail;

namespace api.DTOs.Product
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
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
        public ICollection<OrderDetailDTO>? OrderDetails { get; set; } = new List<OrderDetailDTO>();
        public ICollection<FeedbackDTO>? Feedbacks { get; set; } = new List<FeedbackDTO>();
        public ICollection<NewsDetailDTO>? NewsDetails {get; set;} = new List<NewsDetailDTO>();
        public ICollection<BannerDetailDTO>? BannerDetails {get; set;} = new List<BannerDetailDTO>();
    }
}