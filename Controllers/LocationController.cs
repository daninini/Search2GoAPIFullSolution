using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Manager;
using Search2Go.Application.Interfaces;
using Search2Go.Infrastructure.Helpers;
using Search2Go.Infrastructure.Services;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/locations")]
    [Authorize(Roles = "Company,Admin")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _service;

        public LocationController(ILocationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<LocationDto>>> GetAll(Guid companyId) =>
            Ok(await _service.GetAllAsync(companyId));

        [HttpGet("{id}")]
        public async Task<ActionResult<LocationDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<LocationDto>> Create(Guid companyId, CreateLocationRequest request) =>
            Ok(await _service.CreateAsync(companyId, request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateLocationRequest request)
        {
            var success = await _service.UpdateAsync(id, request);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
        [HttpPost("create")]
        [Authorize(Roles = "User,Company,Seller")]
        public async Task<IActionResult> CreateLocation([FromBody] CreateLocationRequest request)
        {
            Guid userId = User.GetUserId();
            await _service.CreateLocationAsync(userId, request);
            return Ok(new { success = true });
        }
    }
}
