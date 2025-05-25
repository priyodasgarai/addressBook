using addressBook.Dtos.Order;
using addressBook.Models;
using addressBook.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order> CreateAsync(Order OrderModel);
        Task<Order?> UpdateAsync(int id, Order OrderRequestDto);
        Task<IEnumerable<Order>> GetAllAsync();
        Task<Order?> GetByIdAsync(int id);
        Task<List<Order>> GetByUserIdAsync(AppUser appUser);
        Task<Order?> DeleteAsync(int id);
        Task<bool> OrderExists(int id);
    }
}