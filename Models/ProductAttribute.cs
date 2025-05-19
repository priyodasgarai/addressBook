using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace addressBook.Models
{
    [Table("ProductAttributes")]
    public class ProductAttribute
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string SKU { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Size { get; set; }
        public int StockQuantity { get; set; }
        public decimal? Price { get; set; }
        public bool IsDefault { get; set; } = false;
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Product? Product { get; set; }

    }
}