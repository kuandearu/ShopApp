using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.BannerDetail;
using api.Enums;

namespace api.DTOs.Banner
{
    public class BannerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public BannerStatus Status { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ICollection<BannerDetailDTO>? BannerDetails {get; set;} = new List<BannerDetailDTO>();
    }
}