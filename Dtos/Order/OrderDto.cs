using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Dtos.Order
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public int AddressID { get; set; }
        public int TotalItem { get; set; }
        public decimal Amount { get; set; }
        public int OrderStatus { get; set; }
        public int PaymentStatus { get; set; }      
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } 
        public string? UserName { get; set; }

    }
}
