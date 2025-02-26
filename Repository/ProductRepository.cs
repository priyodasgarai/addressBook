using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Data;
using addressBook.Dtos.Product;
using addressBook.Helpers;
using addressBook.Interfaces;
using addressBook.Models;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Repository
{
    public class ProductRepository : IProductRepository
    {
         private readonly ApplicationDBContext _dbContext;
        public ProductRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Product> CreateAsync(Product productModel)
        {
            await _dbContext.Products.AddAsync(productModel);
            await _dbContext.SaveChangesAsync();
            return productModel;

        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var productModel = await _dbContext.Products.FirstOrDefaultAsync(c => c.Id == id);
            if (productModel == null)
            {
                return null;
            }
            _dbContext.Products.Remove(productModel);
            await _dbContext.SaveChangesAsync();
            return productModel;
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
           
            return await _dbContext.Products.Include(s => s.Category).AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(ProductQueryObject query)
        {
          
            var products = _dbContext.Products.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                products = products.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Description))
            {
                products = products.Where(s => s.Description.Contains(query.Description));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDecsending ? products.OrderByDescending(s => s.Name) : products.OrderBy(s => s.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await products.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Product> UpdateAsync(int id, UpdateProductRequestDto productDto)
        {
            var existingProduct = await _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProduct == null)
            {
                return null;
            }
            existingProduct.Name = productDto.Name;
            existingProduct.Description = productDto.Description;
            existingProduct.BrandId = productDto.BrandId;
            existingProduct.CategoryId = productDto.CategoryId;

            await _dbContext.SaveChangesAsync();
            return existingProduct;
        }

        public Task<bool> ProductExists(int id)
        {
            return _dbContext.Products.AnyAsync(x => x.Id == id);
        }

       
    }
}