using System.ComponentModel.DataAnnotations;

namespace Booking_Hotel.DTO
{
    public class TempGuestRoomDto
    {
        public int NumberOfDays { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }
        public string GuestId { get; set; }
        public int RoomId { get; set; }
    }
}
