﻿using System.ComponentModel.DataAnnotations;
using api.Enums;

namespace api.Models
{
    public class User 
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required]
        public UserRole Role { get; set; }

        [StringLength(500, ErrorMessage = "Avatar URL can't be longer than 500 characters.")]
        [RegularExpression(@"^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|svg))$", ErrorMessage = "Invalid Avatar URL format.")]
        public string? Avatar { get; set; }

        [Required]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public int Phone { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

        [DataType(DataType.DateTime)]
        public DateTime? UpdatedAt { get; set; }
        public ICollection<Order>? Orders { get; set; } = new List<Order>();
        public ICollection<Feedback>? Feedbacks { get; set; } = new List<Feedback>();
    }
}
