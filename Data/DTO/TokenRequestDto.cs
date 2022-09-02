using System.ComponentModel.DataAnnotations;

namespace BookingHotel.DTO
{
    public class TokenRequestDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
