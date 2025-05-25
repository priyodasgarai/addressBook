using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Models
{
    [Table("Products")]
    public class Product
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public int BrandId { get; set; }
        [Comment("0=Deactivate,1=Active")]
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Category? Category { get; set; }
        public Brand? Brand { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; } = new List<ProductAttribute>();
        // public List<Comment> Comments { get; set; } = new List<Comment>();

    }
}