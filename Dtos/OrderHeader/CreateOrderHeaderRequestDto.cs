using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace addressBook.Dtos.OrderHeader
{
    public class CreateOrderHeaderRequestDto
    {
        [Required]
        public int OrderID { get; set; }
        [Required]
        public int CartId { get; set; }
        [Required]
        [MaxLength(10, ErrorMessage = "Symbol cannot be over 10 characters")]
        public string ProductName { get; set; }
        [Required]
        [Range(1, 100000000)]
        public decimal ProductPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        [Range(1, 100000000)]
        public decimal Amount { get; set; }
        public int Status { get; set; } = 0;
    }
}
