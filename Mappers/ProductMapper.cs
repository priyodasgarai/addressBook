using addressBook.Dtos.Product;
using addressBook.Models;
namespace addressBook.Mappers
{
    public static class ProductMapper
    {
        public static ProductDto ToProductDto(this Product productModel)
    
        {
            return new ProductDto {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                CategoryId = (int)productModel.CategoryId,
                BrandId = (int)productModel.BrandId,
                CreatedOn = productModel.CreatedOn,               
            };
        } 

        public static Product
        ToProductFromCreateDTO(this CreateProductRequestDto productDTO)
        {
            return new Product {
                Name = productDTO.Name,
                Description = productDTO.Description,
                Status = productDTO.Status,
                CategoryId = productDTO.CategoryId,
                BrandId = productDTO.BrandId
            };
        }
    }
}