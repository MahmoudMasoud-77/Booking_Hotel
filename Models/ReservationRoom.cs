using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Booking_Hotel.Data.Base;

namespace Booking_Hotel.Models
{
    public class ReservationRoom: IEntityBase
    {
        public int Id { get; set; }
        public int NumberOfDays { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateIn { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateOut { get; set; }
        public double TotalPriceForOneRoom { get; set; }

        //Relation

        [ForeignKey("Reservation")]
        public int Reservation_Id { get; set; }
        public virtual Reservation Reservation { get; set; }
        [ForeignKey("Room")]
        public int Room_Id { get; set; }
        public virtual Room Room { get; set; }
    }
}
