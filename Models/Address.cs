
using System.ComponentModel.DataAnnotations.Schema;
using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Models
{
    [Table("Address")]
    public class Address
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public string Name {  get; set; }=string.Empty;
        public string LandMark { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Country { get; set; } = "India";
        public int PinCode {  get; set; }
        public int PhoneNumber {  get; set; }

        [Comment("true=Default address,false=Not Default")]
        public bool IsDefault { get; set; } = false;
        [Comment("0=Deactivate,1=Active")]
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public AppUser? AppUser { get; set; }
    }
}