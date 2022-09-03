using Booking_Hotel.Data.Base;
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
    }
}
