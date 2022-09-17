using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Booking_Hotel.Data.Base;

namespace Booking_Hotel.Models
{
    public class TempGuestRooms: IEntityBase
    {
        public int Id { get; set; }
        [Required]
        public int NumberOfDays { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }

        //Relation
        [ForeignKey("Guest")]
        public string? GuestId { get; set; }
        public virtual Guest? Guest { get; set; }
        public int RoomId { get; set; }
    }
}
