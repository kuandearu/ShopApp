using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Brands
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ShopAppDbContext _context;

        public BrandRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<Brand> CreateBrandAsync(Brand brand)
        {
            await _context.Brands.AddAsync(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<Brand?> DeleteBrandAsync(int id)
        {
            var brand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if(brand == null){
                return null;
            }
            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return brand;
        }

        public async Task<IEnumerable<Brand>> GetAllBrandsAsync()
        {
            return await _context.Brands.Include(b => b.Products).ToListAsync();
        }

        public async Task<Brand?> GetBrandByIdAsync(int id)
        {
            var brand = await _context.Brands.Include(b => b.Products).FirstOrDefaultAsync(b => b.Id == id);
            if(brand == null){
                return null;
            }
            return brand;
        }

        public async Task<Brand?> UpdateBrandAsync(Brand updatedBrand, int id)
        {
            var existedBrand = await _context.Brands.FirstOrDefaultAsync(b => b.Id == id);
            if(existedBrand == null){
                return null;
            }

            existedBrand.Name = updatedBrand.Name;
            existedBrand.Image = updatedBrand.Image;

            await _context.SaveChangesAsync();
            return existedBrand; 
        }
    }
}