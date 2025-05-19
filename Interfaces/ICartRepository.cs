using addressBook.Dtos.Carts;
using addressBook.Dtos.ProductAttribute;
using addressBook.Models;
using addressBook.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace addressBook.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> CreateAsync(Cart cartModel);
        Task<Cart?> UpdateAsync(int id, UpdateCartRequestDto cartRequestDto);
        Task<IEnumerable<Cart>> GetAllAsync();
        Task<Cart?> GetByIdAsync(int id);
        Task<List<Cart>> GetByUserIdAsync(AppUser appUser);
        Task<Cart?> DeleteAsync(int id);
        Task<bool> CartExists(int id);
    }
}