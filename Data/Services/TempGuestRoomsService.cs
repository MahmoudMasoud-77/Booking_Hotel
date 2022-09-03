using Booking_Hotel.Data.Base;
using Booking_Hotel.DTO;
using Booking_Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking_Hotel.Data.Services
{
    public class TempGuestRoomsService : EntityBaseRepository<TempGuestRooms>,ITempGuestRoomsService
    {
        private readonly AppDbContext context;

        public TempGuestRoomsService(AppDbContext _context) : base(_context)
        {
            context = _context;
        }
        public async Task<int> DeleteByGuestID(string id)
        {
            var data = await context.TempGuestRooms.Where(t => t.GuestId == id).ToListAsync();
            if (data != null)
            {
                foreach (var item in data)
                {
                    context.TempGuestRooms.Remove(item);
                }

                return (await context.SaveChangesAsync());
            }
            return 0;
        }
        public async Task<List<TempGuestRooms>> GetAllForGuest(string guestId)
        {
            var data =await context.TempGuestRooms.Where(t => t.GuestId == guestId).ToListAsync();
            return (data);
        }
        public async Task<int> EditTempRoom(int id, TempRoomDTO entity)
        {
            TempGuestRooms tempGuestRooms =await context.TempGuestRooms.FirstOrDefaultAsync(t => t.Id == id);
            if (tempGuestRooms != null)
            {
                tempGuestRooms.NumberOfDays = entity.DateOut.Day - entity.DateIn.Day;

                tempGuestRooms.DateIn = entity.DateIn;
                tempGuestRooms.DateOut = entity.DateOut;
                return (await context.SaveChangesAsync());
            }
            return (0);
        }
        public async Task<bool> CheckIfTempRoomExit(int roomId, string guestId)
        {
            var data = await context.TempGuestRooms.FirstOrDefaultAsync(t => t.RoomId == roomId && t.GuestId == guestId);
            if (data != null)
            {
                return true;
            }
            return false;

        }
    }
}
