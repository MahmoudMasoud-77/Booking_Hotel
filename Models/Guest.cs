using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Booking_Hotel.Models
{
    public class Guest: IdentityUser
    {
        

        //relation
        public virtual ICollection<Reservation> Reservations { get; set; }
        public virtual ICollection<TempGuestRooms> TempGuestRooms { get; set; }
    }
}
