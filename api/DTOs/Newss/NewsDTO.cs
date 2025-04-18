using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.NewsDetail;

namespace api.DTOs.Newss
{
    public class NewsDTO
    {
        public int Id { get; set; } 
        public string Title { get; set; }
        public string? Image { get; set; }
        public string Content { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<NewsDetailDTO>? NewsDetails {get; set;} = new List<NewsDetailDTO>();
    }
}