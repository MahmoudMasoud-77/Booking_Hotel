using System.ComponentModel.DataAnnotations.Schema;
using Booking_Hotel.Data.Base;

namespace Booking_Hotel.Models
{
    public class Room : IEntityBase
    {
        public int Id { get; set; }
        public double Price { get; set; }
        //public StatusRoom Status { get; set; }
        public string Status { get; set; }


        //relation

        [ForeignKey("Branch")]
        public int Branch_Id { get; set; }
        public virtual Branch Branch { get; set; }
        [ForeignKey("Room_Type")]
        public int RoomType_Id { get; set; }
        public virtual RoomType Room_Type { get; set; }

        public virtual ICollection<ReservationRoom> reservationRooms { get; set; }

    }
   
}
