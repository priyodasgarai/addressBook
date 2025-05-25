
using System.ComponentModel.DataAnnotations.Schema;
using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Models
{
    [Table("Carts")]
    public class Cart
    {
        public int Id { get; set; }
        public int ProductAttributeId { get; set; }
        public string AppUserId { get; set; }      
        public int Quantity { get; set; }       
        public bool IsOrder { get; set; } = false;
        [Comment("0=Deactivate,1=Active")]
        public int Status { get; set; } = 0;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public ProductAttribute? ProductAttribute { get; set; }
        public AppUser? AppUser { get; set; }
    }
}