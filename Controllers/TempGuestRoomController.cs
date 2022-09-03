using Booking_Hotel.Data.Services;
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

    }
}
