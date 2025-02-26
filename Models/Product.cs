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

        public int? CategoryId { get; set; }

       //   [Foreignkey("CategoryId")]
        public int? BrandId { get; set; }

        //  [Foreignkey("BrandId")]
        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public int? Status { get; set; }

        public Category? Category { get; set; }

        public Brand? Brand { get; set; }
    }
}