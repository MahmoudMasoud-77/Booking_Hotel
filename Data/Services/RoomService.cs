using Booking_Hotel.Data.Base;
using Booking_Hotel.Data.Enums;
using Booking_Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking_Hotel.Data.Services
{
    public class RoomService : EntityBaseRepository<Room>,IRoomService
    {
        private readonly AppDbContext context;
        public RoomService(AppDbContext _context) : base(_context)
        {
            context = _context;
        }
        public async Task<ICollection<Room>> GetALLAvialable()
        {
            List<Room> rooms = await context.Rooms.Where(r => r.Status == StatusRoom.Available).Include(r => r.Branch).Include(r => r.Room_Type).ToListAsync();
            return rooms;
        }

        public async Task<ICollection<Room>> GetAllRoomByBranchId(int branchId)
        {
            var data = await context.Rooms.Where(r => r.Branch_Id == branchId).Include(r => r.Branch).Include(r => r.Room_Type).ToListAsync();
            if (data != null)
            {
                return data;
            }
            throw new Exception("Branch Empty");
        }
    }
}
