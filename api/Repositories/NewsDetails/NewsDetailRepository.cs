using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.NewsDetails
{
    public class NewsDetailRepository : INewsDetailRepository
    {
        private readonly ShopAppDbContext _context;

        public NewsDetailRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<NewsDetail>> GetAllNewsDetailsAsync()
        {
            return await _context.NewsDetails.Include(nd => nd.Product).Include(nd => nd.News).ToListAsync();
        }

        public async Task<NewsDetail?> GetNewsDetailByIdAsync(int id)
        {
            return await _context.NewsDetails.FirstOrDefaultAsync(nd => nd.Id == id);
        }

        public async Task<NewsDetail> CreateNewsDetailAsync(NewsDetail newsDetail)
        {
            await _context.NewsDetails.AddAsync(newsDetail);
            await _context.SaveChangesAsync();
            return newsDetail;
        }

        public async Task<NewsDetail?> UpdateNewsDetailAsync(NewsDetail updatedNewsDetail, int id)
        {
            var existingNewsDetail = await _context.NewsDetails.FirstOrDefaultAsync(nd => nd.Id == id);
            if (existingNewsDetail == null)
            {
                return null;
            }

            existingNewsDetail.ProductId = updatedNewsDetail.ProductId;
            existingNewsDetail.NewsId = updatedNewsDetail.NewsId;
            existingNewsDetail.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingNewsDetail;
        }

        public async Task<NewsDetail?> DeleteNewsDetailAsync(int id)
        {
            var newsDetail = await _context.NewsDetails.FirstOrDefaultAsync(nd => nd.Id == id);
            if (newsDetail == null)
            {
                return null;
            }

            _context.NewsDetails.Remove(newsDetail);
            await _context.SaveChangesAsync();
            return newsDetail;
        }
    }
}
