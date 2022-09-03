using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public interface IRoomService : IEntityBaseRepository<Room>
    {
        Task<ICollection<Room>> GetALLAvialable();
        Task<ICollection<Room>> GetAllRoomByBranchId(int branchId);
    }
}
