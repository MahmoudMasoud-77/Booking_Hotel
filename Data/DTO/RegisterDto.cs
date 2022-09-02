using System.ComponentModel.DataAnnotations;

namespace BookingHotel.DTO
{
    public class RegisterDto
    {
        public string UserName { get; set; }
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }
        public string Address { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmePassword { set; get; }
    }
}
