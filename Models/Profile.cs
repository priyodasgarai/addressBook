using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Models.Identity;

namespace addressBook.Models
{
    [Table("Profiles")]
    public class Profile
    {
        public string AppUserId { get; set; }
        public int Id { get; set; }
        public AppUser AppUser { get; set; }
        public string? Phone { get; set; }

    }
}