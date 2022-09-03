using Booking_Hotel.Data.Base;
using Booking_Hotel.Data.Enums;
using Booking_Hotel.DTO;
using Booking_Hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace Booking_Hotel.Data.Services
{
    public class ReservationService : EntityBaseRepository<Reservation>,IReservationService
    {
        private readonly AppDbContext context;
        private readonly IRoomService roomService;

        public ReservationService(AppDbContext _context, IRoomService _RoomService) : base(_context)
        {
            context = _context;
            roomService = _RoomService;
        }

        public List<Reservation>GetReservationsForGuest(string guestId)
        {
            var data =  context.Reservations.Where(r => r.Guest_Id == guestId).Include(r => r.ReservationRooms).ThenInclude(r => r.Room).AsSplitQuery().ToList();
            if (data != null)
            {
                return (data);
            }
            throw new Exception("No found reservation for this guest");
        }
        public async Task< List<Reservation>> GetAllReservationNotConfirmed()
        {
            var data =await context.Reservations.Where(r => r.Status == false).ToListAsync();
            if (data != null)
            {
                return (data);
            }
            throw new Exception("No found reservation for this guest");
        }
        public async Task<StatusResponse> ConfirmReservation(int reservationId)
        {
            var data = context.Reservations.Include(r => r.Guest).Include(r => r.ReservationRooms).AsSplitQuery().FirstOrDefault(r => r.Id == reservationId);
            if (data != null)
            {
                data.Status = true;
                foreach (var item in data.ReservationRooms)
                {
                    if (item.DateIn.Date == DateTime.Now.Date)
                    {
                        var result = await roomService.GetByIdAsync(item.Room_Id);
                        result.Status = StatusRoom.Booked;
                    }
                }
                await context.SaveChangesAsync();
                return new StatusResponse { Message = "Booking Done", Status = true };
            }
            return new StatusResponse { Message = $"No reservation found by this id {reservationId} ", Status = false };
        }

        public async Task<bool> CancleReservation(int reservationId, int roomId)
        {
            ReservationRoom reservationRoom;
            var data = context.Reservations.Include(r => r.Guest).Include(r => r.ReservationRooms).AsSplitQuery().FirstOrDefault(r => r.Id == reservationId);
            if (data != null)
            {
                foreach (var item in data.ReservationRooms)
                {
                    if (item.Room_Id == roomId && item.Reservation_Id == reservationId)
                    {
                        var result = await roomService.GetByIdAsync(roomId);
                        result.Status = StatusRoom.Available;
                        reservationRoom = data.ReservationRooms.FirstOrDefault(r => r.Reservation_Id == reservationId && r.Room_Id == roomId);
                        data.ReservationRooms.Remove(reservationRoom);
                        if (data.ReservationRooms.Count == 0)
                        {
                            context.Reservations.Remove(data);
                        }
                        data.TotalPrice -= item.TotalPriceForOneRoom;
                        await context.SaveChangesAsync();
                        return true;
                    }
                }

            }
            return false;
        }

        public async Task<bool> CancleReservationForAllRooms(int reservationId)
        {
            var data = await context.Reservations.FirstOrDefaultAsync(r => r.Id == reservationId);
            if (data != null)
            {
                context.Reservations.Remove(data);
               await context.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}
