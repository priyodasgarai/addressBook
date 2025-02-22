using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using addressBook.Dtos.Identity;
using addressBook.Helpers;
using addressBook.Models.Identity;
using CoreApiResponse;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace addressBook.Controllers.Auth
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : BaseController
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        private readonly IConfiguration _configuration;
        public AccountController(IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userNameExists = await _userManager.FindByNameAsync(model.Username);
            if (userNameExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "User name already exists!" });


            var userEmailExists = await _userManager.FindByEmailAsync(model.Email);
            if (userEmailExists != null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Message = "User email already exists!" });
                // return CustomResult("Invalid Email Id ", HttpStatusCode.BadRequest);
            }

            /*  var user = new IdentityUser
              {
                  UserName = model.Username,
                  Email = model.Email
              };
                 if(user!=null){
                     return CustomResult("UserName or Email Id aldeady exited",  HttpStatusCode.BadRequest);
                 } 
                  //  return CustomResult("uaer",user);
                 */
            var appUser = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
            };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (!result.Succeeded)
            {
                return CustomResult("User registered faild", result.Errors, HttpStatusCode.BadRequest);

            }
            // if(!await _roleManager.RoleExistsAsync(UserRoles.User)){
            //     await _roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            // }
            return CustomResult("User registered Successfully", HttpStatusCode.OK);

        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {

                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>{
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }


                // authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Signingkey"]));
                var token = new JwtSecurityToken(
                                   issuer: _configuration["JWT:Issuer"],
                                   audience: _configuration["JWT:Audience"],
                                   expires: DateTime.Now.AddHours(3),
                                   claims: authClaims,
                                   signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                               );

                var newUser = new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    //  Roles=userRoles.ToList().,
                    Token = new JwtSecurityTokenHandler().WriteToken(token)
                };

                return CustomResult("User login Successfully", newUser, HttpStatusCode.OK);
            }
            return CustomResult("Invalid username", HttpStatusCode.Unauthorized);

        }
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return CustomResult("User Not found", HttpStatusCode.NotFound);
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
                {
                    return CustomResult("User delete Successfully", HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("User delete faild", HttpStatusCode.NotFound);
                }
            }
        }
        [HttpGet("all-user")]

        public async Task<IActionResult> allUser()
        {
            try
            {
                var users = _userManager.Users;
                if (users == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data loaded successfully", users, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}