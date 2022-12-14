using Booking_Hotel.Data.Base;
using Booking_Hotel.Data.Enums;

namespace Booking_Hotel.Models
{
    public class RoomType : IEntityBase
    {
        public int Id { get; set; }
        //public TypeOfRoom TypeRoom { get; set; }
        public string TypeRoom { get; set; }

        public int Capacity { get; set; }

        //Relation
        public virtual ICollection<Room> Rooms { get; set; }
    }
    
}
