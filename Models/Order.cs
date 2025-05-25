using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

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

        [Comment("0=Initialized,1=Order Process,2=Order Accept,3=Order Confirm")]
        public int OrderStatus { get; set; }

        [Comment("0=Not Payment,1=COD,2=Online")]
        public int PaymentStatus { get; set; }

        [Comment("0=Deactivate,1=Active")]
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public AppUser? AppUser { get; set; }
        public Address? Address { get; set; }
    }
}

/*
  var orderHeader = new OrderHeader
                {
                    AppUserId = appUser.Id,
                    OrderDate = DateTime.Now,
                    ShippingAddress = orderRequestDto.ShippingAddress,
                    ShippingCity = orderRequestDto.ShippingCity,
                    ShippingCountry = orderRequestDto.ShippingCountry,
                    ShippingName = orderRequestDto.ShippingName,
                    ShippingPhoneNumber = orderRequestDto.ShippingPhoneNumber,
                    ShippingState = orderRequestDto.ShippingState,
                    ShippingZipCode = orderRequestDto.ShippingZipCode
                };  
 */