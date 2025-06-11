// BookingController.cs (API Layer)
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Booking;
using Search2Go.Application.DTOs.Bookings;
using Search2Go.Application.Interfaces;
using System.Security.Claims;

namespace Search2GoAPI.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    [Authorize(Roles = "Enduser,Company,Admin")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        private Guid GetUserId() => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        [HttpPost("create")]
        public async Task<ActionResult<BookingResponse>> Create([FromBody] CreateBookingRequest request)
        {
            var userId = GetUserId();
            var result = await _bookingService.CreateBookingAsync(userId, request);
            return Ok(result);
        }

        [HttpGet("my-bookings")]
        public async Task<ActionResult<List<BookingResponse>>> GetUserBookings()
        {
            var userId = GetUserId();
            return Ok(await _bookingService.GetBookingsByUserIdAsync(userId));
        }

        [HttpGet("company/{companyId}")]
        public async Task<ActionResult<List<BookingResponse>>> GetCompanyBookings(Guid companyId)
        {
            return Ok(await _bookingService.GetCompanyBookingsAsync(companyId));
        }

        [HttpPost("cancel/{bookingId}")]
        public async Task<IActionResult> Cancel(Guid bookingId)
        {
            await _bookingService.CancelBookingAsync(bookingId);
            return NoContent();
        }
    }
}