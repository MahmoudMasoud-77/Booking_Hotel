using Booking_Hotel.DTO;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.UserService
{
    public interface IAuthService
    {
        Task<AuthDto> Register(RegisterDto model);
        Task<AuthDto> Login(TokenRequestDto model);
        Task<string> AddRole(AddRoleDto model);
        Task<Guest> GetGuestInfo(string userName);
    }
}
