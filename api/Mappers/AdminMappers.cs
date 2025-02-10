using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.Admin;
using api.Enums;
using api.Models;

namespace api.Mappers
{
    public static class AdminMappers
    {
        public static AdminDTO ToAdminDTO(this User user){
            return new AdminDTO {
                Id = user.Id,
                PasswordHash = user.PasswordHash,
                Name = user.Name,
                Role = UserRole.Admin,
                Avatar = user.Avatar,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt
            };
        }

        public static User ToCreateAdminResponseDTO(this CreateAdminREsponseDTO dTO){
            return new User {
                Email = dTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dTO.Password),
                Name = dTO.Name,
                Role = UserRole.Admin,
                Avatar = dTO.Avatar,
                Phone = dTO.Phone,
                CreatedAt = DateTime.UtcNow,
            };
        }

        public static User ToUpdateAdminResponseDTO(this UpdateAdminResponseDTO dTO){
            return new User {
                Email = dTO.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dTO.Password),
                Name = dTO.Name,
                Role = UserRole.Admin,
                Avatar = dTO.Avatar,
                Phone = dTO.Phone,
                UpdatedAt = DateTime.UtcNow,
            };
        }
    }
}