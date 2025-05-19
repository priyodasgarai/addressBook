using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace addressBook.Models.Identity
{
    public class AppUser : IdentityUser
    {
        public List<Address> Addresses { get; set; } = new List<Address>();
        public List<Order> Orders { get; set; } = new List<Order>();
        public List<Cart> Carts { get; set; } = new List<Cart>();

    }
}