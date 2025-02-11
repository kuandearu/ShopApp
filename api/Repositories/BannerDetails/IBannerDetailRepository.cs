using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories.BannerDetails
{
    public interface IBannerDetailRepository
    {
        Task<IEnumerable<BannerDetail>> GetAllBannerDetailsAsync();
        Task<BannerDetail?> GetBannerDetailByIdAsync(int id);
        Task<BannerDetail> CreateBannerDetailAsync(BannerDetail bannerDetail);
        Task<BannerDetail?> UpdateBannerDetailAsync(BannerDetail bannerDetail, int id);
        Task<BannerDetail?> DeleteBannerDetailAsync(int id);
    }
}