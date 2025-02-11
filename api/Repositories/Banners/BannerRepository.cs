using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Banners
{
    public class BannerRepository : IBannerRepository
    {
        private readonly ShopAppDbContext _context;

        public BannerRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Banner>> GetAllBannersAsync()
        {
            return await _context.Banners.Include(b => b.BannerDetails).ToListAsync();
        }

        public async Task<Banner?> GetBannerByIdAsync(int id)
        {
            return await _context.Banners.Include(b => b.BannerDetails)
                                         .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Banner> CreateBannerAsync(Banner banner)
        {
            await _context.Banners.AddAsync(banner);
            await _context.SaveChangesAsync();
            return banner;
        }

        public async Task<Banner?> UpdateBannerAsync(Banner updatedBanner, int id)
        {
            var existingBanner = await _context.Banners.FirstOrDefaultAsync(b => b.Id == id);
            if (existingBanner == null)
            {
                return null;
            }

            existingBanner.Name = updatedBanner.Name;
            existingBanner.Image = updatedBanner.Image;
            existingBanner.Status = updatedBanner.Status;
            existingBanner.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingBanner;
        }

        public async Task<Banner?> DeleteBannerAsync(int id)
        {
            var banner = await _context.Banners.FirstOrDefaultAsync(b => b.Id == id);
            if (banner == null)
            {
                return null;
            }

            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
            return banner;
        }
    }
}
