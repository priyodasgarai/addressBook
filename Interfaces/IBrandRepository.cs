using addressBook.Dtos.Brand;
using addressBook.Helpers;
using addressBook.Models;

namespace addressBook.Interfaces
{
    public interface IBrandRepository
    {
    Task<List<Brand>> GetAllAsync(BrandQueryObject query);
    Task<Brand?> GetByIdAsync(int id);
    Task<Brand> CreateAsync(Brand brandModel);
    Task<Brand?> UpdateAsync(int id, UpdateBrandRequestDto brandDto);
    Task<Brand?> DeleteAsync(int id);
    Task<bool> BrandExists(int id);
    Task<int> BrandCountAsync(BrandQueryObject query);
    }
}