using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.BannerDetails
{
    public class BannerDetailRepository : IBannerDetailRepository
    {
        private readonly ShopAppDbContext _context;

        public BannerDetailRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BannerDetail>> GetAllBannerDetailsAsync()
        {
            return await _context.BannerDetails.Include(bd => bd.Product)
                                                .Include(bd => bd.Banner)
                                                .ToListAsync();
        }

        public async Task<BannerDetail?> GetBannerDetailByIdAsync(int id)
        {
            return await _context.BannerDetails.Include(bd => bd.Product)
                                                .Include(bd => bd.Banner)
                                                .FirstOrDefaultAsync(bd => bd.Id == id);
        }

        public async Task<BannerDetail> CreateBannerDetailAsync(BannerDetail bannerDetail)
        {
            await _context.BannerDetails.AddAsync(bannerDetail);
            await _context.SaveChangesAsync();
            return bannerDetail;
        }

        public async Task<BannerDetail?> UpdateBannerDetailAsync(BannerDetail updatedBannerDetail, int id)
        {
            var existingBannerDetail = await _context.BannerDetails.FirstOrDefaultAsync(bd => bd.Id == id);
            if (existingBannerDetail == null)
            {
                return null;
            }

            existingBannerDetail.ProductId = updatedBannerDetail.ProductId;
            existingBannerDetail.BannerId = updatedBannerDetail.BannerId;
            existingBannerDetail.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingBannerDetail;
        }

        public async Task<BannerDetail?> DeleteBannerDetailAsync(int id)
        {
            var bannerDetail = await _context.BannerDetails.FirstOrDefaultAsync(bd => bd.Id == id);
            if (bannerDetail == null)
            {
                return null;
            }

            _context.BannerDetails.Remove(bannerDetail);
            await _context.SaveChangesAsync();
            return bannerDetail;
        }
    }
}
