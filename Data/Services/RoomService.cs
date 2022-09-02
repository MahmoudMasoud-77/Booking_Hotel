using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public class RoomService : EntityBaseRepository<Room>,IRoomService
    {
        public RoomService(AppDbContext _context) : base(_context)
        {
        }
    }
}
