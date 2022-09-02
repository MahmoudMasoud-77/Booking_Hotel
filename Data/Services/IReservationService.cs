using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public interface IReservationService: IEntityBaseRepository<Reservation>
    {
    }
}
