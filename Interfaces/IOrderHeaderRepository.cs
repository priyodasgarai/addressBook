using addressBook.Dtos.OrderHeader;
using addressBook.Models;
using addressBook.Models.Identity;

namespace addressBook.Interfaces
{
    public interface IOrderHeaderRepository
    {
        Task<OrderHeader> CreateAsync(OrderHeader orderHeaderModel);
        Task<OrderHeader?> UpdateAsync(int id, UpdateOrderHeaderRequestDto OrderHeaderRequestDto);
        Task<IEnumerable<OrderHeader>> GetAllAsync();
        Task<List<OrderHeader>> GetByOrderIdAsync(Order order);
        Task<OrderHeader?> GetByIdAsync(int id);      
        Task<OrderHeader?> DeleteAsync(int id);
    }
}