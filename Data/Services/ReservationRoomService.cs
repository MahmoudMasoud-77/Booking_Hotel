using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public class ReservationRoomService : EntityBaseRepository<ReservationRoom>,IReservationRoomService
    {
        public ReservationRoomService(AppDbContext _context) : base(_context)
        {
        }
    }
}
