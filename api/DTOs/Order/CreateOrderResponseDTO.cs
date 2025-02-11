using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs.Order
{
    public class CreateOrderResponseDTO
    {
        public int UserId { get; set; }
        public string? Note { get; set; }
        public int Total { get; set; }
    }
}