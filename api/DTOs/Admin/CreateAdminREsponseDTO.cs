using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.DTOs.Admin
{
    public class CreateAdminREsponseDTO
    {

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
        public string Email { get; set; }

        [Required]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
        public string Password { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        // [Required]
        // public UserRole Role { get; set; }

        [StringLength(500, ErrorMessage = "Avatar URL can't be longer than 500 characters.")]
        [RegularExpression(@"^(https?:\/\/.*\.(?:png|jpg|jpeg|gif|svg))$", ErrorMessage = "Invalid Avatar URL format.")]
        public string? Avatar { get; set; }

        [Required]
        public int Phone { get; set; }

    }
}