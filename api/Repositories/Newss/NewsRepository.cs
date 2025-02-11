using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Newss
{
    public class NewsRepository : INewsRepository
    {
        private readonly ShopAppDbContext _context;

        public NewsRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<News>> GetAllNewsAsync()
        {
            return await _context.News.ToListAsync();
        }

        public async Task<News?> GetNewsByIdAsync(int id)
        {
            return await _context.News.FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task<News> CreateNewsAsync(News news)
        {
            await _context.News.AddAsync(news);
            await _context.SaveChangesAsync();
            return news;
        }

        public async Task<News?> UpdateNewsAsync(News updatedNews, int id)
        {
            var existingNews = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if (existingNews == null)
            {
                return null;
            }

            existingNews.Title = updatedNews.Title;
            existingNews.Image = updatedNews.Image;
            existingNews.Content = updatedNews.Content;
            existingNews.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingNews;
        }

        public async Task<News?> DeleteNewsAsync(int id)
        {
            var news = await _context.News.FirstOrDefaultAsync(n => n.Id == id);
            if (news == null)
            {
                return null;
            }

            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return news;
        }
    }
}
