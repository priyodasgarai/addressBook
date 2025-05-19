using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Models.Identity;

namespace addressBook.Models
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public int? AddressID { get; set; }
        public int TotalItem { get; set; }
        public decimal? Amount { get; set; }
        public int OrderStatus { get; set; }
        public int PaymentStatus { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public AppUser? AppUser { get; set; }
        public Address? Address { get; set; }
    }
}