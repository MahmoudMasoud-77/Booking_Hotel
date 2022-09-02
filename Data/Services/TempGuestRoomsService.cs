using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public class TempGuestRoomsService : EntityBaseRepository<TempGuestRooms>,ITempGuestRoomsService
    {
        public TempGuestRoomsService(AppDbContext _context) : base(_context)
        {
        }
    }
}
