using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Feedbacks
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ShopAppDbContext _context;

        public FeedbackRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks.Include(f => f.Product).Include(f => f.User).ToListAsync();
        }

        public async Task<Feedback?> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks.Include(f => f.Product).Include(f => f.User).FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Feedback> CreateFeedbackAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<Feedback?> UpdateFeedbackAsync(Feedback feedback, int id)
        {
            var existingFeedback = await _context.Feedbacks.FindAsync(id);
            if (existingFeedback == null)
            {
                return null;
            }

            existingFeedback.Star = feedback.Star;
            existingFeedback.Content = feedback.Content;
            existingFeedback.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingFeedback;
        }

        public async Task<Feedback?> DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
            {
                return null;
            }

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }
    }
}
