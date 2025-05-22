using addressBook.Dtos.Carts;
using addressBook.Dtos.ProductAttribute;
using addressBook.Models;

namespace addressBook.Mappers
{
    public static class CartMapper
    {
        public static CartDto ToCartDto(this Cart cartModel)
        {
            return new CartDto
            {
                Id = cartModel.Id,
                ProductAttributeId = cartModel.ProductAttributeId,
                AppUserId = cartModel.AppUserId,
                Quantity = cartModel.Quantity,
                Status = cartModel.Status,
                CreatedOn = cartModel.CreatedOn,
                SKU = cartModel.ProductAttribute?.SKU,
                ProductName=cartModel.ProductAttribute.Product?.Name,
                Unit = cartModel.ProductAttribute?.Unit,               
                Price = cartModel.ProductAttribute?.Price,
                Size=(int)cartModel.ProductAttribute?.Size,
            };
        }
        //public static Cart ToCartCreateDTO(this CreateCartRequestDto cartRequestDto)
        //{
        //    return new Cart
        //    {
        //        ProductAttributeId = cartRequestDto.ProductAttributeId,               
               
        //    };
        //}
    }
}
