using addressBook.Dtos.Address;
using addressBook.Dtos.Carts;
using addressBook.Dtos.Category;
using addressBook.Extension;
using addressBook.Interfaces;
using addressBook.Mappers;
using addressBook.Models;
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
        private readonly IAddressRepository _addressRepo;
        private readonly UserManager<AppUser> _userManager;
        public WebCartController(ICartRepository cartRepo,
            ILogger<WebCartController> logger,
            UserManager<AppUser> userManager,
            IAddressRepository addressRepo
            )
        {
            _cartRepo = cartRepo;
            _logger = logger;
            _userManager = userManager;
            _addressRepo = addressRepo;
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
            catch (Exception ex)
            {
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
        public async Task<IActionResult> Create([FromBody] CreateCartRequestDto CartRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);

                var username = User.GetUsername();
                var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                if (appUser == null)
                {
                    return CustomResult("User not found", HttpStatusCode.NotFound);
                }
                //    return CustomResult("Data attributeId", CartRequestDto.ProductAttributeId, HttpStatusCode.OK);
                var cartModel = new Cart
                {
                    ProductAttributeId = CartRequestDto.ProductAttributeId,
                    AppUserId = appUser.Id,
                    Quantity = 1
                };
                //  return CustomResult("Data added successfully", cartModel,HttpStatusCode.OK);
                var productDetails = await _cartRepo.ProductExit(CartRequestDto.ProductAttributeId, appUser.Id);

                if (productDetails != null)
                {
                    if (productDetails.Quantity <= 4)
                    {
                        var Quantity = productDetails.Quantity + 1;
                        var UpdateCartModel = await _cartRepo.UpdateQuantityAsync(productDetails.Id, Quantity);
                        return CustomResult("Data Update successfully", UpdateCartModel, HttpStatusCode.OK);
                    }
                    else
                    {
                        return CustomResult("You can add max five product", productDetails, HttpStatusCode.Conflict);
                    }
                }

                await _cartRepo.CreateAsync(cartModel);
                return CustomResult("Data added successfully", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPost()]
        [Route("Decrease-Card")]
        public async Task<IActionResult> decreaseCard([FromBody] CreateCartRequestDto CartRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);

                var username = User.GetUsername();
                var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                if (appUser == null)
                {
                    return CustomResult("User not found", HttpStatusCode.NotFound);
                }
                var productDetails = await _cartRepo.ProductExit(CartRequestDto.ProductAttributeId, appUser.Id);

                if (productDetails == null)
                {
                    return CustomResult("Item not found", HttpStatusCode.NotFound);
                }


                if (productDetails.Quantity > 1)
                {
                    var Quantity = productDetails.Quantity - 1;
                    var UpdateCartModel = await _cartRepo.UpdateQuantityAsync(productDetails.Id, Quantity);
                    return CustomResult("Data Update successfully", UpdateCartModel, HttpStatusCode.OK);
                }
                else if (productDetails.Quantity == 1)
                {
                    var cartModel = await _cartRepo.DeleteAsync(productDetails.Id);
                    if (cartModel == null)
                    {
                        return CustomResult("Data not found", HttpStatusCode.NotFound);
                    }

                    return CustomResult("Data delete successfully", HttpStatusCode.OK);
                }
                else
                {
                    return CustomResult("You can add max five product", productDetails, HttpStatusCode.Conflict);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            try
            {
                var cartModel = await _cartRepo.DeleteAsync(id);
                if (cartModel == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data delete successfully", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCartRequestDto cartRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
                var cartModel = await _cartRepo.UpdateAsync(id, cartRequestDto);
                if (cartModel == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data Updated successfully", cartModel.ToCartDto(), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [Route("new-Address")]
        public async Task<IActionResult> NewAddress([FromBody] CreateAddressRequestDto addressDto)
        {
            try
            {

                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);

                var username = User.GetUsername();
                var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                if (appUser == null)
                {
                    return CustomResult("User not found", HttpStatusCode.NotFound);
                }
                var addressModel = new Address
                {
                    Name = addressDto.Name,
                    AppUserId = appUser.Id,
                    LandMark = addressDto.LandMark,
                    City = addressDto.City,
                    State = addressDto.State,
                    PinCode = addressDto.PinCode,
                    PhoneNumber = addressDto.PhoneNumber,
                    Status = addressDto.Status
                };
                await _addressRepo.CreateAsync(addressModel);
                return CustomResult("Data added successfully", addressModel.ToAddressDto(), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpGet]
        [Route("all-address")]
        public async Task<IActionResult> allAddress()
        {
            try
            {
                var username = User.GetUsername();
                var appUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == username);
                if (appUser == null)
                {
                    return CustomResult("User not found", HttpStatusCode.NotFound);
                }
                // return await _addressRepo.GetByUserIdAsync(appUser);

                var Address = await _addressRepo.GetByUserIdAsync(appUser);
                if (Address == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                var addressDto = Address.Select(c => c.ToAddressDto()).ToList();
                return CustomResult("Data loaded successfully", addressDto, HttpStatusCode.OK);

            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpDelete]
        [Route("delete-address/{id:int}")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            try
            {
                var addressModel = await _addressRepo.DeleteAsync(id);
                if (addressModel == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }

                return CustomResult("Data delete successfully", HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
        [HttpPut]
        [Route("update-address/{id:int}")]
        public async Task<IActionResult> updateAddress([FromRoute] int id, [FromBody] UpdateAddressRequestDto addressRequestDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return CustomResult("One or more validation errors occurred", ModelState, HttpStatusCode.BadRequest);
                var addressModel = await _addressRepo.UpdateAsync(id, addressRequestDto);
                if (addressModel == null)
                {
                    return CustomResult("Data not found", HttpStatusCode.NotFound);
                }
                return CustomResult("Data Updated successfully", addressModel.ToAddressDto(), HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                // _logger.LogInformation(ex.Message);
                _logger.LogError(ex.Message);
                return CustomResult(ex.Message, HttpStatusCode.BadRequest);
            }
        }
    }
}
