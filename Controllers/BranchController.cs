using Booking_Hotel.Data.Services;
using Booking_Hotel.Data;
using Booking_Hotel.Models;
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
        [HttpGet("{id:int}", Name = "GetOneBranch")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var branch = await Service.GetByIdAsync(id);
                if (branch != null)
                {
                    return Ok(branch);
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
                var result = await Service.GetByIdAsync(id);
                if(result != null)
                {
                    await Service.DeleteAsync(id);
                    return Ok(new StatusResponse { Message = $"Branch deleted", Status = true });
                }
                return BadRequest(new StatusResponse { Message = "Not found any branch", Status = false });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Insert(Branch model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await Service.InsertAsync(model);
                    return Ok();
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPatch]
        public async Task<IActionResult> Edit(int id, Branch model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await Service.GetByIdAsync(id);
                    if (result != null)
                    {
                        await Service.UpdateAsync(id, model);
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
    }
}
