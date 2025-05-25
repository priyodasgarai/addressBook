using System.ComponentModel.DataAnnotations;

namespace addressBook.Dtos.OrderHeader
{
    public class UpdateOrderHeaderRequestDto
    {
        public decimal ProductPrice { get; set; }
     
        public int Quantity { get; set; }
       
        public decimal Amount { get; set; }
        public int Status { get; set; } 
    }
}
