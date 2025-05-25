using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Models
{
    [Table("OrderHeaders")]
    public class OrderHeader
    {
        public int Id { get; set; }
        public int? OrderID { get; set; }
        public int CartId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal? ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Amount { get; set; }

        [Comment("0=Deactivate,1=Active")]
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public Order? Order { get; set; }
        public Cart? cart { get; set; }
    }
}