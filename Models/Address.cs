
using System.ComponentModel.DataAnnotations.Schema;
using addressBook.Models.Identity;

namespace addressBook.Models
{
    [Table("Address")]
    public class Address
    {
        public int Id { get; set; }
        public string? AppUserId { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public AppUser? AppUser { get; set; }
    }
}