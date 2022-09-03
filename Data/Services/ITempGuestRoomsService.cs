using Booking_Hotel.Data.Base;
using Booking_Hotel.DTO;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public interface ITempGuestRoomsService : IEntityBaseRepository<TempGuestRooms>
    {
        Task<int> DeleteByGuestID(string id);
        Task<List<TempGuestRooms>> GetAllForGuest(string guestId);
        Task<int> EditTempRoom(int id, TempRoomDTO entity);
        Task<bool> CheckIfTempRoomExit(int roomId, string guestId);

    }
}
