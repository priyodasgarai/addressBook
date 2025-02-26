
using addressBook.Dtos.Brand;
using addressBook.Models;

namespace addressBook.Mappers
{
    public static class BrandMapper
    {
         public static BrandDto ToBrandDto(this Brand brandModel)
        {
            return new BrandDto
            {
                Id = brandModel.Id,
                Name = brandModel.Name,
                Image = brandModel.Image,
                CreatedOn = brandModel.CreatedOn,
               // Products = brandModel.Products.Select(c => c.ToProductDto()).ToList()
            };
        }
        public static Brand ToBrandFromCreateDTO(this CreateBrandRequestDto brandDTO)
        {
            return new Brand
            {
                Name = brandDTO.Name,
                Image = brandDTO.Image
            };
        }
        public static Brand ToBrandFromUpdateDto(this UpdateBrandRequestDto brandDTO)
        {

            return new Brand
            {
                Name = brandDTO.Name,
                Image = brandDTO.Image,
            };
        }
    }
}