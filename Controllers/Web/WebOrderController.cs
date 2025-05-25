using addressBook.Data;
using addressBook.Dtos.Carts;
using addressBook.Dtos.Order;
using addressBook.Extension;
using addressBook.Interfaces;
using addressBook.Mappers;
using addressBook.Models;
using addressBook.Models.Identity;
using CoreApiResponse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Transactions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace addressBook.Controllers.Web
{
    [Authorize(Roles = "User")]
    [Route("api/[controller]")]
    [ApiController]
    public class WebOrderController : BaseController

    {
        private readonly IOrderRepository _orderRepo;
        private readonly IOrderHeaderRepository _headerRepo;
        private readonly ILogger<WebOrderController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly ICartRepository _cartRepo;
        private readonly ApplicationDBContext _dbContext;
        public WebOrderController(IOrderHeaderRepository headerRepo,
            IOrderRepository orderRepo,
            ILogger<WebOrderController> logger,
            ICartRepository cartRepo,
             UserManager<AppUser> userManager,
             ApplicationDBContext dBContext
            )
            {
                _headerRepo = headerRepo;
                _orderRepo = orderRepo;
                _cartRepo = cartRepo;
                _logger = logger;
                _userManager = userManager;
            _dbContext = dBContext;
            }
        [HttpPost]
        [Route("new-order")]
        public async Task<IActionResult> addOrder([FromBody] CreateOrderRequestDto orderRequestDto) {
            using (var transaction = _dbContext.Database.BeginTransaction())
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
                    var cartDto = Cart.Select(c => c.ToCartDto()).ToList();
                    if (cartDto == null)
                    {
                        return CustomResult("Data not found", HttpStatusCode.NotFound);
                    }
                    var orderModel = new Order
                    {
                        OrderStatus = orderRequestDto.OrderStatus,
                        AppUserId = appUser.Id,
                        PaymentStatus = orderRequestDto.PaymentStatus,
                        Status = orderRequestDto.Status
                    };
                    await _orderRepo.CreateAsync(orderModel);

                    if (orderModel == null)
                    {
                        return CustomResult("Data not found", HttpStatusCode.NotFound);
                    }

                    var totalItem = 0;
                    var totalAmount = 0;
                    foreach (var item in cartDto)
                    {
                        var orderHeaderData = new OrderHeader
                        {
                            OrderID = orderModel.Id,
                            CartId = item.Id,
                            ProductName = item.ProductName,
                            ProductPrice = item.Price,
                            Quantity = item.Quantity,
                            Amount = item.Price * item.Quantity,
                            Status = 0,
                        };
                        await _headerRepo.CreateAsync(orderHeaderData);

                        totalItem++;
                        totalAmount = totalAmount + ((int)item.Price * item.Quantity);
                    }
                    var orderData = new Order
                    {

                        TotalItem = totalItem,
                        Amount = totalAmount,
                        OrderStatus = 1,
                        Status = 1
                    };

                    var updatedOrder = await _orderRepo.UpdateAsync(orderModel.id, orderData);


                    //if (orderModel.Id > 0)
                    //{


                    //}
                    return CustomResult("Data loaded successfully", updatedOrder, HttpStatusCode.OK);
                    // var cartDto = Cart.Select(c => c.ToCartDto()).ToList();
                    // return CustomResult("Data loaded successfully", cartDto, HttpStatusCode.OK);
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    _logger.LogInformation(ex.Message);
                    _logger.LogError(ex.Message);
                    return CustomResult(ex.Message, HttpStatusCode.BadRequest);
                }
            }
        }
    }
}
