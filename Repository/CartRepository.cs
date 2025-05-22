using addressBook.Data;
using addressBook.Dtos.Carts;
using addressBook.Dtos.ProductAttribute;
using addressBook.Interfaces;
using addressBook.Models;
using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public CartRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async Task<bool> CartExists(int id)
        {
            return await _dbContext.Carts.AnyAsync(c => c.Id == id);
        }

        public async Task<Cart> CreateAsync(Cart cartModel)
        {
            await _dbContext.Carts.AddAsync(cartModel);
            await _dbContext.SaveChangesAsync();
            return cartModel;
        }

        public async Task<Cart?> DeleteAsync(int id)
        {
            var cartDetails = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cartDetails == null) {
                return null;
                    }
            _dbContext.Carts.Remove(cartDetails);
            await _dbContext.SaveChangesAsync();
            return cartDetails;
        }

        public async Task<IEnumerable<Cart>> GetAllAsync()
        {
           return await _dbContext.Carts.Include(a=>a.ProductAttribute).ThenInclude(p=>p.Product).ToListAsync();
        }

        public async Task<Cart?> GetByIdAsync(int id)
        {
            return await _dbContext.Carts.Include(a => a.ProductAttribute).ThenInclude(p => p.Product).FirstOrDefaultAsync(c => c.Id == id); 
        }

        public async Task<List<Cart>> GetByUserIdAsync(AppUser appUser)
        {
            return await _dbContext.Carts.Include(a => a.ProductAttribute).ThenInclude(p => p.Product).Where(c => c.AppUserId == appUser.Id).Where(c => c.IsOrder==false).ToListAsync();
        }

        public async Task<Cart?> ProductExit(int productAttributeId, string userId)
        {
            return await _dbContext.Carts.FirstOrDefaultAsync(c => c.ProductAttributeId == productAttributeId && c.AppUserId == userId && c.IsOrder==false);
            
        }

        public async Task<Cart?> UpdateAsync(int id, UpdateCartRequestDto cartRequestDto)
        {
            var cartDetails = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cartDetails != null)
            {
                return null;
            }
            cartDetails.Quantity = cartRequestDto.Quantity;
            cartDetails.IsOrder = cartRequestDto.IsOrder;
            cartDetails.Status = cartRequestDto.Status;
            await _dbContext.SaveChangesAsync();
            return cartDetails;
        }

        public async Task<Cart?> UpdateQuantityAsync(int id, int quantity)
        {
            var cartDetails = await _dbContext.Carts.FirstOrDefaultAsync(c => c.Id == id);
            if (cartDetails == null)
            {
                return null;
            }
            cartDetails.Quantity = quantity;           
            await _dbContext.SaveChangesAsync();
            return cartDetails;
        }
    }
}
