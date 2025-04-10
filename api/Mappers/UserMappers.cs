using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTOs.User;
using api.Models;

namespace api.Mappers
{
    public static class UserMappers
    {
        public static UserDTO ToUserDTO(this User user){
            return new UserDTO{
                Id = user.Id,
                Email = user.Email,
                PasswordHash = user.PasswordHash,
                Name = user.Name,
                Role = user.Role,
                Avatar = user.Avatar,
                Phone = user.Phone,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                Orders = user.Orders?.Select(o => o.ToOrderDTO()).ToList(),
                Feedbacks = user.Feedbacks?.Select(f => f.ToFeedbackDTO()).ToList(),
            };
        }
    }
}