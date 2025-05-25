using System.ComponentModel.DataAnnotations;

namespace addressBook.Dtos.Order
{
    public class CreateOrderRequestDto
    {
        [Required]
        public int AddressID { get; set; }
        [Required]
        public int TotalItem { get; set; }
        [Required]
        [Range(1, 100000000)]
        public decimal Amount { get; set; }
        public int OrderStatus { get; set; }= 0;
        [Required]
        public int PaymentStatus { get; set; }
        [Required]
        public int Status { get; set; }
    }
}
