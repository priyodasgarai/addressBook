using addressBook.Data;
using addressBook.Dtos.Category;
using addressBook.Dtos.ProductAttribute;
using addressBook.Interfaces;
using addressBook.Models;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Repository
{
    public class ProductAttributeRepository : IProductAttributeRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public ProductAttributeRepository(ApplicationDBContext dbContext)
        {
                _dbContext = dbContext;
        }
        public async Task<ProductAttribute> CreateAsync(ProductAttribute productAttribute)
        {
            await _dbContext.ProductAttributes.AddAsync(productAttribute);
            await _dbContext.SaveChangesAsync();
            return productAttribute;
        }

        public async Task<ProductAttribute?> DeleteAsync(int id)
        {
            var productAttributeModel = await _dbContext.ProductAttributes.FirstOrDefaultAsync(c => c.Id == id);
            if (productAttributeModel == null)
            {
                return null;
            }
            // return categoryModel;
            _dbContext.ProductAttributes.Remove(productAttributeModel);
            await _dbContext.SaveChangesAsync();
            return productAttributeModel;
        }

        public async Task<IEnumerable<ProductAttribute>> GetAllAsync()
        {
            return await _dbContext.ProductAttributes.Include(p => p.Product).ToListAsync();
        }

        public async Task<ProductAttribute?> GetByIdAsync(int id)
        {
            return await _dbContext.ProductAttributes.Include(p => p.Product).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ProductAttributeExists(int id)
        {
            return await _dbContext.ProductAttributes.AnyAsync(x => x.Id == id);
        }

        public async Task<ProductAttribute?> UpdateAsync(int id, UpdateProductAttributeRequestDto productAttributeDto)
        {
            var existingProductAttribute = await _dbContext.ProductAttributes.FirstOrDefaultAsync(x => x.Id == id);
            if (existingProductAttribute == null)
            {
                return null;
            }          
            existingProductAttribute.SKU = productAttributeDto.SKU;
            existingProductAttribute.Unit = productAttributeDto.Unit;
            existingProductAttribute.Size = productAttributeDto.Size;
            existingProductAttribute.StockQuantity = productAttributeDto.StockQuantity;
            existingProductAttribute.Price = productAttributeDto.Price;
            existingProductAttribute.IsDefault = productAttributeDto.IsDefault;
            existingProductAttribute.Status = productAttributeDto.Status;           
            await _dbContext.SaveChangesAsync();
            return existingProductAttribute;
        }
    }
}
