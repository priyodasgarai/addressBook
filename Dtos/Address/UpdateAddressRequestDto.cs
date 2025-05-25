using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Dtos.Address
{
    public class UpdateAddressRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public string LandMark { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;      
        public int PinCode { get; set; }
        public int PhoneNumber { get; set; }
        public bool IsDefault { get; set; } 
        public int Status { get; set; }
       
    }
}