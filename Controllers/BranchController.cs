using Booking_Hotel.Data.Services;
using Booking_Hotel.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Booking_Hotel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : ControllerBase
    {
        private readonly IBranchService Service;

        public BranchController(IBranchService _Service)
        {
            this.Service = _Service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var data = await Service.GetAllAsync();
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
    }
}
