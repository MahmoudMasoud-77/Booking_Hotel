using Microsoft.AspNetCore.Mvc;
using Booking_Hotel.Data.Services;
using Booking_Hotel.Data;
using Booking_Hotel.DTO;
using Booking_Hotel.Models;
using Microsoft.AspNetCore.Authorization;

namespace Booking_Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ReservationController : ControllerBase
    {
        private readonly IReservationService ReservationService;
        private readonly IReservationRoomService ReservationRoomService;
        private readonly IRoomService RoomService;
        private readonly ITempGuestRoomsService TempGuestRoomsService;
        public ReservationController(IReservationService _ReservationService,
                                     IRoomService _RoomService,
                                     IReservationRoomService _ReservationRoomService,
                                     ITempGuestRoomsService _TempGuestRoomsService)
        {
            this.ReservationService = _ReservationService;
            this.RoomService = _RoomService;
            this.ReservationRoomService = ReservationRoomService;
            this.TempGuestRoomsService = _TempGuestRoomsService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data =await ReservationService.GetAllAsync();
                if (data != null)
                {
                    return Ok(data);
                }
                return NotFound(new StatusResponse { Message = "No data found", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id:int}", Name = "GetOneReservation")]
        public IActionResult GetById(int id)
        {
            try
            {
                var reservation = ReservationService.GetByIdAsync(id);
                if (reservation != null)
                {
                    return Ok(reservation);
                }
                return NotFound(new StatusResponse { Message = "faild no found this branch", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await ReservationService.GetByIdAsync(id);
                if (result != null)
                {
                    await ReservationService.DeleteAsync(id);
                    return Ok(new StatusResponse { Message = $"Reservation deleted", Status = true });
                }
                return BadRequest(new StatusResponse { Message = "Not found any Reservation", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(ReservationDto model)
        {
            Room room;
            double sumTotalPrice = 0;
            Reservation newreservation = new Reservation();
            try
            {
                if (ModelState.IsValid)
                {
                    newreservation.Guest_Id = model.Guest_Id;
                    newreservation.Date = DateTime.Now;
                    newreservation.TotalPrice = 0;
                    ReservationService.InsertAsync(newreservation);
                    foreach (var item in model.ReservationRoomInfo)
                    {
                        room =await RoomService.GetByIdAsync(item.RoomId);
                        ReservationRoomService.InsertAsync(new ReservationRoom
                        {
                            Reservation_Id = newreservation.Id,
                            Room_Id = item.RoomId,
                            DateIn = item.DateIn,
                            TotalPriceForOneRoom = item.NumberOfDays * room.Price,
                            NumberOfDays = item.NumberOfDays,
                            DateOut = item.DateOut,
                        });

                        sumTotalPrice += item.NumberOfDays * room.Price;
                    }
                    if (CheckGeustIfBookingBefore(model.Guest_Id) > 1)
                    {
                        newreservation.TotalPrice = (95.0 / 100.0) * sumTotalPrice;
                    }
                    else
                    {
                        newreservation.TotalPrice = sumTotalPrice;
                    }
                    ReservationService.UpdateAsync(newreservation.Id, newreservation);
                    TempGuestRoomsService.DeleteByGuestID(model.Guest_Id);

                    return Ok(newreservation);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [NonAction]
        private int CheckGeustIfBookingBefore(string guestId)
        {
            try
            {
                List<Reservation> reservation = ReservationService.GetReservationsForGuest(guestId);
                if (reservation != null)
                {
                    return reservation.Count;

                }
                return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        [HttpPatch]
        public async Task<IActionResult> Edit(int id, Reservation model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await ReservationService.GetByIdAsync(id);
                    if (result != null)
                    {
                        await ReservationService.UpdateAsync(id, model);
                        string url = Url.Link("GetOneBranch", new { id = id });
                        return Created(url, model);
                    }
                }
                return BadRequest(new StatusResponse { Message = "Edit faild", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ConfirmReservation/{id}")]
        public async Task<IActionResult> ConfirmReservation(int id)
        {
            try
            {
                var result = await ReservationService.ConfirmReservation(id);
                if (result.Status == true)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CancleReservationForOneRoom")]
        public async Task<IActionResult> CancleReservationForOneRoom(int reservationId, int roomId)
        {
            try
            {
                var result =await ReservationService.CancleReservation(reservationId, roomId);
                if (result)
                {
                    return Ok(new StatusResponse { Message = "Cancel success", Status = true });
                }
                return BadRequest(new StatusResponse { Message = "Cancel faild", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("CancleReservationGuestForAllRooms")]
        public async Task<IActionResult> CancleReservationGuestForAllRooms(int reservationId)
        {
            try
            {
                var result = await ReservationService.CancleReservationForAllRooms(reservationId);
                if (result)
                {
                    return Ok(new StatusResponse { Message = "Reservation deleted", Status = true });
                }
                return BadRequest(new StatusResponse { Message = "Deleted faild", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ReservationDetailsForGuest/{guestId}")]
        public IActionResult ReservationDetailsForGuest(string guestId)
        {
            try
            {
                var data = ReservationService.GetReservationsForGuest(guestId);
                if (data != null)
                {
                    return Ok(data);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
