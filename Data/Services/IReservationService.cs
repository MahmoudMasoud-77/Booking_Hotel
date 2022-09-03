using Booking_Hotel.Data.Base;
using Booking_Hotel.DTO;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public interface IReservationService: IEntityBaseRepository<Reservation>
    {
        List<Reservation> GetReservationsForGuest(string guestId);
        Task<List<Reservation>> GetAllReservationNotConfirmed();
        Task<StatusResponse> ConfirmReservation(int reservationId);
        Task<bool> CancleReservation(int reservationId, int roomId);
        Task<bool> CancleReservationForAllRooms(int reservationId);
    }
}
