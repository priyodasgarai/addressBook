using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using addressBook.Dtos.Identity;

namespace addressBook.Interfaces
{
    public interface IRoleRepository
    {
        Task<List<RoleDto>> GetRolesAsync();
        Task<List<string>> GetUserRolesAsync(string emailId);
        Task<List<string>> AddRolesAsync(string[] roles);
        Task<bool> DeleteRolesAsync(string id);
        Task<bool> AddUserRoleAsync(string urerEmail, string[] roles);
    }
}