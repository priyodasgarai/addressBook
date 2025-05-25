using addressBook.Data;
using addressBook.Dtos.Address;
using addressBook.Interfaces;
using addressBook.Models;
using addressBook.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace addressBook.Repository
{
    public class AddressRepositor : IAddressRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public AddressRepositor(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        public async  Task<bool> AddressExists(int id)
        {
            return await _dbContext.Address.AnyAsync(a => a.Id == id);
        }
        public async  Task<Address> CreateAsync(Address addressModel)
        {
            await _dbContext.Address.AddAsync(addressModel);
            await _dbContext.SaveChangesAsync();
            return addressModel;
        }
        public async Task<Address?> DeleteAsync(int id)
        {
            var addressDetails = await _dbContext.Address.FirstOrDefaultAsync(c => c.Id == id);
            if (addressDetails == null)
            {
                return null;
            }
            _dbContext.Address.Remove(addressDetails);
            await _dbContext.SaveChangesAsync();
            return addressDetails;
        }
        public async  Task<IEnumerable<Address>> GetAllAsync()
        {
            return await _dbContext.Address.Include(a => a.AppUser).ToListAsync();
        }

        public async  Task<Address?> GetByIdAsync(int id)
        {
            return await _dbContext.Address.Include(a => a.AppUser).FirstOrDefaultAsync(c => c.Id == id);
        }
        public async  Task<List<Address>> GetByUserIdAsync(AppUser appUser)
        {
            return await _dbContext.Address.Include(a => a.AppUser).Where(c => c.AppUserId == appUser.Id).ToListAsync();
        }
        public async  Task<Address?> UpdateAsync(int id, UpdateAddressRequestDto AddressRequestDto)
        {
            var addressDetails = await _dbContext.Address.FirstOrDefaultAsync(c => c.Id == id);
            if (addressDetails == null)
            {
                return null;
            }           
            addressDetails.Name = AddressRequestDto.Name;
            addressDetails.LandMark = AddressRequestDto.LandMark;
            addressDetails.City = AddressRequestDto.City;
            addressDetails.State = AddressRequestDto.State;
            addressDetails.PinCode = AddressRequestDto.PinCode;
            addressDetails.PhoneNumber = AddressRequestDto.PhoneNumber;
            addressDetails.IsDefault = AddressRequestDto.IsDefault;           
            addressDetails.Status = AddressRequestDto.Status;
            await _dbContext.SaveChangesAsync();
            return addressDetails;
        }
    }
}
