using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories.Banners
{
    public interface IBannerRepository
    {
         Task<IEnumerable<Banner>> GetAllBannersAsync();
        Task<Banner?> GetBannerByIdAsync(int id);
        Task<Banner> CreateBannerAsync(Banner banner);
        Task<Banner?> UpdateBannerAsync(Banner banner, int id);
        Task<Banner?> DeleteBannerAsync(int id);
    }
}