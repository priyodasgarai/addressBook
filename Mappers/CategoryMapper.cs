using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Dtos.Category;
using addressBook.Models;

namespace addressBook.Mappers
{
    public static class CategoryMapper
    {
         public static CategoryDto ToCategoryDto(this Category categoryModel)
        {
            return new CategoryDto
            {
                Id = categoryModel.Id,
                Name = categoryModel.Name,
                Description = categoryModel.Description,
                Image = categoryModel.Image,
                ParentId = categoryModel.ParentId,
                CreatedOn = categoryModel.CreatedOn,
                Status=categoryModel.Status,
              //  Products = categoryModel.Products.Select(c => c.ToProductDto()).ToList()
            };
        }
        public static Category ToCategoryFromCreateDTO(this CreateCtegoryRequestDto categoryDTO)
        {
            return new Category
            {
                Name = categoryDTO.Name,
                Description = categoryDTO.Description,
                ParentId = categoryDTO.ParentId,
                Image = categoryDTO.Image,
                Status = categoryDTO.Status,
            };
        }
    }
}