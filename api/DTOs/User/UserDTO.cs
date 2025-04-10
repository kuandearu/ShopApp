using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Feedback;
using api.DTOs.Order;
using api.Enums;

namespace api.DTOs.User
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
        public string? Avatar { get; set; }
        public int Phone { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public ICollection<OrderDTO>? Orders { get; set; } = new List<OrderDTO>();
        public ICollection<FeedbackDTO>? Feedbacks { get; set; } = new List<FeedbackDTO>();
    }
}