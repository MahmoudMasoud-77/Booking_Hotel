using Booking_Hotel.Data.Services;
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
                
                    return Ok(data);
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
