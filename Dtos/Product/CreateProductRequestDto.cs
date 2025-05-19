using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Dtos.Product
{
    public class CreateProductRequestDto
    {
        
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int CategoryId { get; set; }

        public int BrandId { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int Status { get; set; }
    }
}