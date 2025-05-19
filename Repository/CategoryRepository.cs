using addressBook.Data;
using addressBook.Dtos.Category;
using addressBook.Helpers;
using addressBook.Interfaces;
using addressBook.Models;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<int> CategoryCountAsync(CategoryQueryObject query)
        {
            var categories = _dbContext.categories.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                categories = categories.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Description))
            {
                categories = categories.Where(s => s.Description.Contains(query.Description));
            }
             return await categories.CountAsync();
        }

        public Task<bool> CategoryExists(int id)
        {
            return _dbContext.categories.AnyAsync(x => x.Id == id);
        }

        public async Task<Category> CreateAsync(Category categoryModel)
        {
            await _dbContext.categories.AddAsync(categoryModel);
            await _dbContext.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<Category?> DeleteAsync(int id)
        {
            var categoryModel = await _dbContext.categories.FirstOrDefaultAsync(c => c.Id == id);
            if (categoryModel == null)
            {
                return null;
            }
           // return categoryModel;
            _dbContext.categories.Remove(categoryModel);
            await _dbContext.SaveChangesAsync();
            return categoryModel;
        }

        public async Task<List<Category>> GetAllAsync(CategoryQueryObject query)
        {
            //return await _dbContext.categories.ToListAsync();
            var categories = _dbContext.categories.Include(p => p.Products).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                categories = categories.Where(s => s.Name.Contains(query.Name));
            }
            if (!string.IsNullOrWhiteSpace(query.Description))
            {
                categories = categories.Where(s => s.Description.Contains(query.Description));
            }
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    categories = query.IsDecsending ? categories.OrderByDescending(s => s.Name) : categories.OrderBy(s => s.Name);
                }
                if (query.SortBy.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    categories = query.IsDecsending ? categories.OrderByDescending(s => s.Description) : categories.OrderBy(s => s.Description);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;            
            return await categories.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _dbContext.categories.Include(p => p.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Category?> UpdateAsync(int id, UpdateCategoryRequestDto categoryDto)
        {
            var existingCategory = await _dbContext.categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCategory == null)
            {
                return null;
            }
            existingCategory.Name = categoryDto.Name;
            existingCategory.Description = categoryDto.Description;
            existingCategory.ParentId = categoryDto.ParentId;
            existingCategory.Image = categoryDto.Image;

            await _dbContext.SaveChangesAsync();
            return existingCategory;
        }
    }
}
