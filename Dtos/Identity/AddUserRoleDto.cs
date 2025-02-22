using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Dtos.Identity
{
    public class AddUserRoleDto
    {
        public string? UserEmail { get; set; }
        public string[]? Roles { get; set; }
    }
}