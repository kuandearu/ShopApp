using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.NewsDetail
{
    public class NewsDetailDTO
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int NewsId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}