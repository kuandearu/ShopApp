using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs;
using api.Models;

namespace api.Mappers
{
    public static class AuthMappers
    {
        public static User ToRegisterAccountResponse(this RegisterDTO registerDTO){
            return new User {
                
                Email = registerDTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDTO.Password),
                Name = registerDTO.Name,
                Avatar = registerDTO.Avatar,
                Phone = registerDTO.PhoneNumber,
                CreatedAt = DateTime.UtcNow,

            };
        }
    }
}