using addressBook.Data;
using addressBook.Dtos.Order;
using addressBook.Interfaces;
using addressBook.Models;
using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public OrderRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;            
        }
        public async Task<Order> CreateAsync(Order OrderModel)
        {
            await _dbContext.Orders.AddAsync(OrderModel);
            await _dbContext.SaveChangesAsync();
            return OrderModel;
        }

        public async Task<Order?> DeleteAsync(int id)
        {
            var orderModel = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (orderModel == null)
            {
                return null;
            }
            _dbContext.Orders.Remove(orderModel);
            await _dbContext.SaveChangesAsync();
            return orderModel;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _dbContext.Orders.Include(c => c.Address).Include(a => a.AppUser).ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _dbContext.Orders.Include(c => c.Address).Include(a => a.AppUser).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Order>> GetByUserIdAsync(AppUser appUser)
        {
            return await _dbContext.Orders.Include(c => c.Address).Include(a => a.AppUser).Where(c => c.AppUserId == appUser.Id).ToListAsync();
        }

        public async Task<bool> OrderExists(int id)
        {
            return await _dbContext.Orders.AnyAsync(c => c.Id == id);
        }

        public async Task<Order?> UpdateAsync(int id, Order OrderRequestDto)
        {
            var orderDetails = await _dbContext.Orders.FirstOrDefaultAsync(c => c.Id == id);
            if (orderDetails == null)
            {
                return null;
            }
            orderDetails.AddressID = OrderRequestDto.AddressID;
            orderDetails.OrderStatus = OrderRequestDto.OrderStatus;
            orderDetails.PaymentStatus = OrderRequestDto.PaymentStatus;
            orderDetails.Amount = OrderRequestDto.Amount;
            orderDetails.TotalItem = OrderRequestDto.TotalItem;
            orderDetails.Status = OrderRequestDto.Status;
            await _dbContext.SaveChangesAsync();
            return orderDetails;
        }
    }
}
