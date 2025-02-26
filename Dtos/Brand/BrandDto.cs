using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Models;

namespace addressBook.Dtos.Brand
{
    public class BrandDto
    {
           public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Image { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;
        
        public int? Status { get; set; }

        // public List<Product> Products { get; set; } = new List<Product>();
    }
}