

using addressBook.Dtos.ProductAttribute;

namespace addressBook.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int Status { get; set; }
        public string? CategoryName { get; set; } 

        public string? BrandName { get; set; }
        //public List<CommentDto> Comments{ get; set; } 
        public List<ProductAttributeDto> productAttributes { get; set; } 

    }
}