using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Product;

namespace api.DTOs.Brand
{
    public class BrandDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Image { get; set; }
        public ICollection<ProductDTO>? Products { get; set; } = new List<ProductDTO>();
    }
}