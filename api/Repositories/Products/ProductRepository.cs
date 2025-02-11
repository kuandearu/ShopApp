using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.Data;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories.Products
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopAppDbContext _context;

        public ProductRepository(ShopAppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> UpdateProductAsync(Product product, int id)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (existingProduct == null)
            {
                return null;
            }

            // Update the fields
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.OldPrice = product.OldPrice;
            existingProduct.Image = product.Image;
            existingProduct.Description = product.Description;
            existingProduct.Specification = product.Specification;
            existingProduct.BuyTurn = product.BuyTurn;
            existingProduct.Quantity = product.Quantity;
            existingProduct.BrandId = product.BrandId;
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return existingProduct;
        }

        public async Task<Product?> DeleteProductAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            if (product == null)
            {
                return null;
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
