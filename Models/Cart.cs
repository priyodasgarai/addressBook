
using System.ComponentModel.DataAnnotations.Schema;
using addressBook.Models.Identity;

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
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public ProductAttribute? ProductAttribute { get; set; }
        public AppUser? AppUser { get; set; }
    }
}