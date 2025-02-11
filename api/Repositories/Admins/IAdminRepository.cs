using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories.Admins
{
    public interface IAdminRepository
    {
        Task<IEnumerable<User>> GetAllAdminsAsync();
        Task<User?> GetAdminByIdAsync(int id);
        Task<User> CreateAdminAsync(User user);
        Task<User?> UpdateAdminAsync(User user, int id);
        Task<User?> DeleteAdminAsync(int id); 
    }
}