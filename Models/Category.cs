using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Models
{
    [Table("Categories")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string Image { get; set; } = string.Empty;
        [Comment("0=Deactivate,1=Active")]
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public List<Product> Products { get; set; } = new List<Product>();
    }
}