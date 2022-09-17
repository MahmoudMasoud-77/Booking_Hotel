using System.ComponentModel.DataAnnotations;

namespace Booking_Hotel.DTO
{
    //loginDto
    public class TokenRequestDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
