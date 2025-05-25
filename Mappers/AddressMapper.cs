using addressBook.Dtos.Address;

using addressBook.Models;

namespace addressBook.Mappers
{
    public static class AddressMapper
    {
        public static AddressDto ToAddressDto(this Address addressModel)
        {
            return new AddressDto
            {
                Id = addressModel.Id,
                AppUserId = addressModel.AppUserId,
                Name = addressModel.Name,
                LandMark = addressModel.LandMark,
                City = addressModel.City,
                State = addressModel.State,
                Country = addressModel.Country,
                PinCode = addressModel.PinCode,
                PhoneNumber = addressModel.PhoneNumber,
                IsDefault = addressModel.IsDefault,
                Status = addressModel.Status,
                CreatedOn = addressModel.CreatedOn,
                UserName = addressModel.AppUser?.UserName,
               
            };
        }
    }
}
