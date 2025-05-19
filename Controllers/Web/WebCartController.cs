using addressBook.Dtos.Carts;
using addressBook.Extension;
using addressBook.Interfaces;
using addressBook.Mappers;
using addressBook.Models.Identity;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace addressBook.Controllers.Web
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class WebCartController : BaseController

    {
        private readonly ICartRepository _cartRepo;
        private readonly ILogger<WebCartController> _logger;

        private readonly UserManager<AppUser> _userManager;
        public WebCartController(ICartRepository cartRepo,ILogger<WebCartController> logger, UserManager<AppUser> userManager)
        {
            _cartRepo = cartRepo;
            _logger = logger;      
            _userManager = userManager;
           
        }
        [HttpGet]
        [Route("User-Details")]
        public async Task<IActionResult> userDetais()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (appUser == null)
            {
                return CustomResult("User not found", HttpStatusCode.NotFound);
            }
            return CustomResult("Data loaded successfully", appUser.Id, HttpStatusCode.OK);           
        }
      
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
                var Cart = await _cartRepo.GetByIdAsync(id);
                if (Cart == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data loaded successfully", Cart.ToCartDto(), HttpStatusCode.OK);

            }
            catch (Exception ex) {
                _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
            }
        [HttpGet()]
        public async Task<IActionResult> GetByGetByUserId()
        {
            try
            {
                var username = User.GetUsername();
                var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                if (appUser == null)
                {
                    return CustomResult("User not found", HttpStatusCode.NotFound);
                }

                var Cart = await _cartRepo.GetByUserIdAsync(appUser);
                if (Cart == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                var cartDto = Cart.Select(c => c.ToCartDto()).ToList();
                return CustomResult("Data loaded successfully", cartDto, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> Create( [FromBody] CreateCartRequestDto cartRequestDto)
        {
            try
            {      
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);

                var cartModel = cartRequestDto.ToCartCreateDTO();
              
                await _cartRepo.CreateAsync(cartModel);
                var result = CreatedAtAction(nameof(GetById), new { id = cartModel.Id }, cartModel.ToCartDto());
                return CustomResult("Data added successfully", result.Value, HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
