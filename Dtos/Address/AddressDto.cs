using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Dtos.Address
{
    public class AddressDto
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string LandMark { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = "India";
        public int PinCode { get; set; }
        public int PhoneNumber { get; set; }        
        public bool IsDefault { get; set; } = false;      
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public string? UserName { get; set; }
    }
}