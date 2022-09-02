using System.ComponentModel.DataAnnotations;

namespace BookingHotel.DTO
{
    public class TempRoomDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }

    }
}