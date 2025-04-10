using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly ShopAppDbContext _context;

        public UserRepository(ShopAppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.Include(u => u.Orders).Include(u => u.Feedbacks).ToListAsync();
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.Include(u => u.Orders).Include(u => u.Feedbacks).FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}