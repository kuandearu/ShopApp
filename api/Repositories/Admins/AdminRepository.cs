using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Enums;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Admins
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ShopAppDbContext _context;

        public AdminRepository(ShopAppDbContext context)
        {
            _context = context;
        }
        public async Task<User> CreateAdminAsync([FromBody]User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> DeleteAdminAsync(int id)
        {
            var admin = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(admin == null){
                return null;
            }
            _context.Users.Remove(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<User?> GetAdminByIdAsync(int id)
        {
            var admin = await _context.Users.Include(u => u.Orders)
                                            .ThenInclude(o => o.OrderDetails)
                                            .Include(u => u.Feedbacks).
                                            FirstOrDefaultAsync(u => u.Id == id);
            if(admin == null){
                return null;
            }
            return admin;
        }

        public async Task<IEnumerable<User>> GetAllAdminsAsync()
        {
            return await _context.Users.Include(u => u.Orders)
                                        .ThenInclude(o => o.OrderDetails)
                                        .Include(u => u.Feedbacks)
                                        .Where(u => u.Role == UserRole.Admin)
                                        .ToListAsync();
        }

        public async Task<User?> UpdateAdminAsync([FromBody]User updatedAdmin, int id)
        {
            var existingAdmin = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if(existingAdmin == null){
                return null;
            }

            existingAdmin.Email = updatedAdmin.Email;
            existingAdmin.PasswordHash = updatedAdmin.PasswordHash;
            existingAdmin.Name = updatedAdmin.Name;
            existingAdmin.Role = UserRole.Admin;
            existingAdmin.Avatar = updatedAdmin.Avatar;
            existingAdmin.Phone = updatedAdmin.Phone;
            existingAdmin.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingAdmin;
        }
    }
}