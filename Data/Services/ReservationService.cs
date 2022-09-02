using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public class ReservationService : EntityBaseRepository<Reservation>,IReservationService
    {
        public ReservationService(AppDbContext _context) : base(_context)
        {
        }
    }
}
