using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Booking;
using Search2Go.Application.Interfaces;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _service;

        public BookingController(IBookingService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "EndUser")]
        public async Task<IActionResult> Create(CreateBookingRequest request)
        {
            var id = await _service.CreateAsync(request);
            return CreatedAtAction(nameof(GetByUser), new { userId = request.UserId }, id);
        }

        [HttpGet("user/{userId}")]
        [Authorize(Roles = "EndUser")]
        public async Task<IActionResult> GetByUser(Guid userId)
        {
            var bookings = await _service.GetByUserAsync(userId);
            return Ok(bookings);
        }

        [HttpPost("cancel/{id}")]
        [Authorize(Roles = "EndUser")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _service.CancelAsync(id);
            if (!result) return NotFound();
            return Ok("Cancelled");
        }
    }
}
