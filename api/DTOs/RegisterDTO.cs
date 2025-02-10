using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using api.Enums;

namespace api.DTOs
{
    public class RegisterDTO
    {
        
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        [StringLength(256, ErrorMessage = "Email cannot exceed 256 characters.")]
        public string Email { get; set; }
        public string Name { get; set; }
        [MinLength(6, ErrorMessage = "Password has to be atleast 6 characters")]
        public string Password { get; set; }

        public string? Avatar { get; set; }

        public int PhoneNumber { get; set; }
    }
}