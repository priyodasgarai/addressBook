using addressBook.Dtos.ProductAttribute;
using addressBook.Helpers;
using addressBook.Models;

namespace addressBook.Interfaces
{
    public interface IProductAttributeRepository
    {
        Task<ProductAttribute> CreateAsync(ProductAttribute productAttribute);
        Task<ProductAttribute?> UpdateAsync(int id, UpdateProductAttributeRequestDto productAttributeDto);
        Task<IEnumerable<ProductAttribute>> GetAllAsync();
        Task<ProductAttribute?> GetByIdAsync(int id);
        Task<ProductAttribute?> DeleteAsync(int id);
        Task<bool> ProductAttributeExists(int id);
       
    }
}
