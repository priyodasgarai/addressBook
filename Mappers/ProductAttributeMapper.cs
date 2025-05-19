using addressBook.Dtos.Category;
using addressBook.Dtos.ProductAttribute;
using addressBook.Models;

namespace addressBook.Mappers
{
    public static class ProductAttributeMapper
    {

        public static ProductAttributeDto ToProductAttributeDto(this ProductAttribute productAttributeModel)
        {
            return new ProductAttributeDto
            {
                Id = productAttributeModel.Id,
                ProductId=productAttributeModel.ProductId,
                SKU=productAttributeModel.SKU,
                Unit = productAttributeModel.Unit,
                Size = productAttributeModel.Size,
                StockQuantity = productAttributeModel.StockQuantity,
                Price = productAttributeModel.Price,
                IsDefault = productAttributeModel.IsDefault,
                Status = productAttributeModel.Status,
                CreatedOn = productAttributeModel.CreatedOn,
                ProductName=    productAttributeModel.Product?.Name,

            };
        }
        public static ProductAttribute ToProductAttributeFromCreateDTO(this CreateProductAttributeRequestDto productAttributeDTO)
        {
            return new ProductAttribute
            {
                ProductId = productAttributeDTO.ProductId,
                SKU = productAttributeDTO.SKU,
                Unit = productAttributeDTO.Unit,
                Size = productAttributeDTO.Size,
                StockQuantity = productAttributeDTO.StockQuantity,
                Price = productAttributeDTO.Price,
                IsDefault = productAttributeDTO.IsDefault=false,
                Status = productAttributeDTO.Status,
            };
        }

    }
}
