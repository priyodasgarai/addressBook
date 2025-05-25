using addressBook.Data;
using addressBook.Dtos.OrderHeader;
using addressBook.Interfaces;
using addressBook.Models;
using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Repository
{
    public  class OrderHeaderRepository : IOrderHeaderRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public  OrderHeaderRepository(ApplicationDBContext dBContext)
        {
                _dbContext = dBContext;

        }
        public async Task<OrderHeader> CreateAsync(OrderHeader orderHeaderModel)
        {
            await _dbContext.OrderHeaders.AddAsync(orderHeaderModel);
            await _dbContext.SaveChangesAsync();
            return orderHeaderModel;
        }

        public async Task<OrderHeader?> DeleteAsync(int id)
        {
           var orderHeaderModel = await _dbContext.OrderHeaders.FirstOrDefaultAsync(x => x.Id == id);
            if (orderHeaderModel == null)
            {
                return null;
            }
            _dbContext.OrderHeaders.Remove(orderHeaderModel);
            await _dbContext.SaveChangesAsync();
            return orderHeaderModel;

        }

        public async Task<IEnumerable<OrderHeader>> GetAllAsync()
        {
            return await _dbContext.OrderHeaders.Include(c => c.cart).Include(o => o.Order).ToListAsync();
        }

        public async Task<OrderHeader?> GetByIdAsync(int id)
        {
            return await _dbContext.OrderHeaders.Include(c => c.cart).Include(o => o.Order).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<OrderHeader>> GetByOrderIdAsync(Order order)
        {
            return await _dbContext.OrderHeaders.Include(c => c.cart).Include(o => o.Order).Where(c => c.OrderID == order.Id).Where(c => c.Status == 0).ToListAsync();
        }

        public async Task<OrderHeader?> UpdateAsync(int id, UpdateOrderHeaderRequestDto OrderHeaderRequestDto)
        {
            var orderHeaderModel = await _dbContext.OrderHeaders.FirstOrDefaultAsync(c => c.Id == id);
            if (orderHeaderModel == null)
            {
                return null;
            }
            orderHeaderModel.ProductPrice = OrderHeaderRequestDto.ProductPrice;
            orderHeaderModel.Quantity = OrderHeaderRequestDto.Quantity;
            orderHeaderModel.Amount = OrderHeaderRequestDto.Amount;
            orderHeaderModel.Status = OrderHeaderRequestDto.Status;
            await _dbContext.SaveChangesAsync();
            return orderHeaderModel;
        }
    }
}
