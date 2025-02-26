using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Data;
using addressBook.Dtos.Brand;
using addressBook.Helpers;
using addressBook.Interfaces;
using addressBook.Models;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Repository
{
    public class BrandRepository : IBrandRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public BrandRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public Task<bool> BrandExists(int id)
        {
            return _dbContext.Brands.AnyAsync(x => x.Id == id);
        }

        public async Task<Brand> CreateAsync(Brand brandModel)
        {
            await _dbContext.Brands.AddAsync(brandModel);
            await _dbContext.SaveChangesAsync();
            return brandModel;
        }

        public async Task<Brand?> DeleteAsync(int id)
        {
            var brandModel = await _dbContext.Brands.FirstOrDefaultAsync(c => c.Id == id);
            if (brandModel == null)
            {
                return null;
            }
            _dbContext.Brands.Remove(brandModel);
            await _dbContext.SaveChangesAsync();
            return brandModel;
        }

        public async Task<List<Brand>> GetAllAsync(BrandQueryObject query)
        {
            var brands = _dbContext.Brands.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                brands = brands.Where(s => s.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    brands = query.IsDecsending ? brands.OrderByDescending(s => s.Name) : brands.OrderBy(s => s.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await brands.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Brand?> GetByIdAsync(int id)
        {
          //  return await _dbContext.Brands.Include(p => p.Products).FirstOrDefaultAsync(b => b.Id == id);
          return await _dbContext.Brands.Include(p => p.Products).FirstOrDefaultAsync(c => c.Id == id); 
        }

        public async Task<Brand?> UpdateAsync(int id,UpdateBrandRequestDto brandDto)
        {
            var existingBrand = await _dbContext.Brands.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBrand == null)
            {
                return null;
            }
            existingBrand.Name = brandDto.Name;
            existingBrand.Image = brandDto.Image;
            await _dbContext.SaveChangesAsync();
            return existingBrand;
        }
    
    }
}