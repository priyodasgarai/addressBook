using addressBook.Models.Identity;

namespace addressBook.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
