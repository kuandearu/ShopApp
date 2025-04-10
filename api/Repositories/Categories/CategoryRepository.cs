using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Categories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ShopAppDbContext _context;

        public CategoryRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category?> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(category == null){
                return null;
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            var category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
            if(category == null){
                return null;
            }
            return category;
        }

        public async Task<Category?> UpdateCategoryAsync(Category updatedCategory, int id)
        {
            var existedCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
            if(existedCategory == null){
                return null;
            }

            existedCategory.Name = updatedCategory.Name;
            existedCategory.Image = updatedCategory.Image;

            await _context.SaveChangesAsync();
            return existedCategory; 
        }
    }
}