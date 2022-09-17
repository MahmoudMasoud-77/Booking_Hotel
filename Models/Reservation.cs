using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Booking_Hotel.Data.Base;

namespace Booking_Hotel.Models
{
    public class Reservation: IEntityBase
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public double TotalPrice { get; set; }

        //Relation

        [ForeignKey("Guest")]
        public string? Guest_Id { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual ICollection<ReservationRoom> ReservationRooms { get; set; }
    }
}
