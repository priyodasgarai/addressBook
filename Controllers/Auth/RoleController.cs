using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using addressBook.Dtos.Identity;
using addressBook.Interfaces;
using CoreApiResponse;
using Microsoft.AspNetCore.Mvc;

namespace addressBook.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : BaseController
    {
        private readonly IRoleRepository _roleRepository;
        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles()
        {
            var list = await _roleRepository.GetRolesAsync();
            return CustomResult("Data loaded successfully", list, HttpStatusCode.OK);
        }


        [HttpGet("GetUserRole")]
        public async Task<IActionResult> GetUserRole(string userEmail)
        {
            var userClaims = await _roleRepository.GetUserRolesAsync(userEmail);
            return CustomResult("Data loaded successfully", userClaims, HttpStatusCode.OK);
        }

        [HttpPost("addRoles")]
        public async Task<IActionResult> AddRole(string[] roles)
        {
            var userrole = await _roleRepository.AddRolesAsync(roles);
            if (userrole == null)
            {
                return CustomResult("Role did not found!", HttpStatusCode.BadRequest);
            }
            return CustomResult("Data loaded successfully", userrole, HttpStatusCode.OK);
        }
           [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteRole([FromRoute] string id)
        {
         var userrole = await _roleRepository.DeleteRolesAsync(id);
            if (userrole)
            {
                return CustomResult("Role delete successfully",  HttpStatusCode.OK);
            }           
             return CustomResult("Role  not found!", HttpStatusCode.BadRequest);
        }


        [HttpPost("addUserRoles")]
        public async Task<IActionResult> AddUserRole([FromBody] NewUserRoleDto addUser)
        {
            var result = await _roleRepository.AddUserRoleAsync(addUser.UserEmail, addUser.Roles);
            if (!result)
            {
                return CustomResult("Role did not found!", HttpStatusCode.BadRequest);
            }
            return CustomResult("Data added successfully", result, HttpStatusCode.OK);
        }
    }
}