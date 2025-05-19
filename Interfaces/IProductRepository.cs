using addressBook.Dtos.Product;
using addressBook.Helpers;
using addressBook.Models;

namespace addressBook.Interfaces
{
    public interface IProductRepository
    {
        Task<Product> CreateAsync(Product product);
        Task<Product?> UpdateAsync(int id, UpdateProductRequestDto productDto);
        Task<IEnumerable<Product>> GetAllAsync(ProductQueryObject query);
        Task<Product?> GetByIdAsync(int id);
        Task<Product?> DeleteAsync(int id);
        Task<bool> ProductExists(int id);
        Task<int> ProductCountAsync(ProductQueryObject query);
    }
}