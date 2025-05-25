
using addressBook.Dtos.Address;
using addressBook.Models;
using addressBook.Models.Identity;

namespace addressBook.Interfaces
{
    public interface IAddressRepository
    {
        Task<Address> CreateAsync(Address addressModel);
        Task<Address?> UpdateAsync(int id, UpdateAddressRequestDto AddressRequestDto);
        Task<IEnumerable<Address>> GetAllAsync();
        Task<Address?> GetByIdAsync(int id);
        Task<List<Address>> GetByUserIdAsync(AppUser appUser);
        Task<Address?> DeleteAsync(int id);
        Task<bool> AddressExists(int id);
    }
}
