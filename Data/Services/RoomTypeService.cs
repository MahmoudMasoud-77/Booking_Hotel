using Booking_Hotel.Data.Base;
using Booking_Hotel.Models;

namespace Booking_Hotel.Data.Services
{
    public class RoomTypeService : EntityBaseRepository<RoomType>,IRoomTypeService
    {
        public RoomTypeService(AppDbContext _context) : base(_context)
        {
        }
    }
}
