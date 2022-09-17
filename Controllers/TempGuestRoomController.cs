using Booking_Hotel.Data.Services;
using Booking_Hotel.Data;
using Booking_Hotel.DTO;
using Booking_Hotel.Models;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempGuestRoomController : ControllerBase
    {
        private readonly ITempGuestRoomsService service;
        public TempGuestRoomController(ITempGuestRoomsService _service)
        {
            this.service = _service;
        }
        [HttpGet("GetAllTempByGuestId")]
        public  async Task<IActionResult> GetAllTempByGuestId(string guestId)
        {
            try
            {
                var data = await service.GetAllForGuest(guestId);
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
        [HttpGet("GetTempRoomByID")]
        public async Task<IActionResult> GetTempRoomByID(int id)
        {
            try
            {
                var data =await service.GetByIdAsync(id);
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
        [HttpDelete("DeleteTempRoomByID")]
        public async Task<IActionResult> DeleteTempRoomByID(int id)
        {
            try
            {
                var result = await service.GetByIdAsync(id);
                if(result !=null)
                {
                    await service.DeleteAsync(id);
                    return Ok(new StatusResponse { Message = "Delete succsess", Status = true });
                }
                return BadRequest(new StatusResponse { Message = "Delete faild", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(TempGuestRoomDto model)
        {
            TempGuestRooms newTempGuestRooms = new TempGuestRooms();
            newTempGuestRooms.DateIn = model.DateIn;
            newTempGuestRooms.DateOut = model.DateIn;
            newTempGuestRooms.NumberOfDays = model.NumberOfDays;
            newTempGuestRooms.RoomId = model.RoomId;
            newTempGuestRooms.GuestId = model.GuestId;
            try
            {
                if (ModelState.IsValid)
                {
                    await service.InsertAsync(newTempGuestRooms);
                    return Ok(model);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public IActionResult Edit(int id, TempRoomDTO tempGuestRooms)
        {
            try
            {
                var data = service.EditTempRoom(id, tempGuestRooms);
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
        [HttpGet]
        public async Task<IActionResult> CheckTempRoomExist(int roomId, string guestId)
        {
            try
            {
                var data =await service.CheckIfTempRoomExit(roomId, guestId);
                if (data)
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteByGuestID(string id)
        {
            try
            {
                var data =await service.DeleteByGuestID(id);
                if (data > 1)
                {
                    return Ok(true);
                }
                return Ok(false);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
