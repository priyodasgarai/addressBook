using addressBook.Dtos.Category;
using addressBook.Helpers;
using addressBook.Models;

namespace addressBook.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync(CategoryQueryObject query);
        Task<Category?> GetByIdAsync(int id);
        Task<Category> CreateAsync(Category categoryModel);
        Task<Category?> UpdateAsync(int id, UpdateCategoryRequestDto categoryDto);
        Task<Category?> DeleteAsync(int id);
         Task<bool> CategoryExists(int id);
        Task<int> CategoryCountAsync(CategoryQueryObject query);
    }
}