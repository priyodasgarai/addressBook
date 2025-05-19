using addressBook.Models.Identity;

namespace addressBook.Dtos.Carts
{
    public class CartDto
    {
        public int Id { get; set; }
        public int ProductAttributeId { get; set; }
        public string? AppUserId { get; set; }
        public int Quantity { get; set; }
        public bool IsOrder { get; set; } = false;
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string SKU { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;
        public int Size { get; set; }
        public decimal? Price { get; set; }
        public string? ProductName { get; set; }
       
    }
}
