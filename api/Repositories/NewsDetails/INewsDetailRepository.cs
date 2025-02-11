using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Repositories.NewsDetails
{
    public interface INewsDetailRepository
    {
        Task<IEnumerable<NewsDetail>> GetAllNewsDetailsAsync();
        Task<NewsDetail?> GetNewsDetailByIdAsync(int id);
        Task<NewsDetail> CreateNewsDetailAsync(NewsDetail newsDetail);
        Task<NewsDetail?> UpdateNewsDetailAsync(NewsDetail newsDetail, int id);
        Task<NewsDetail?> DeleteNewsDetailAsync(int id);
    }
}