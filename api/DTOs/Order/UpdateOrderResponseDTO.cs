using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs.Order
{
    public class UpdateOrderResponseDTO
    {
        public OrderStatus Status { get; set; } = OrderStatus.Confirmed;
        public string? Note { get; set; }

    }
}