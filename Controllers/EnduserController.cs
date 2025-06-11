// EnduserController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Search2Go.Application.DTOs.Enduser;
using Search2Go.Application.Interfaces;
using System.Security.Claims;

namespace Search2GoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Enduser")]
    public class EnduserController : ControllerBase
    {
        private readonly IEnduserService _enduserService;

        public EnduserController(IEnduserService enduserService)
        {
            _enduserService = enduserService;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _enduserService.GetProfileAsync(Guid.Parse(userId));
            return Ok(result);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromForm] UpdateEnduserProfileRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _enduserService.UpdateProfileAsync(Guid.Parse(userId), request);
            return Ok(result);
        }

        [HttpPost("profile/upload")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile file)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var url = await _enduserService.UploadProfilePictureAsync(Guid.Parse(userId), file);
            return Ok(new { ImageUrl = url });
        }

        [HttpGet("bookings")]
        public async Task<IActionResult> GetBookings()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _enduserService.GetBookingsAsync(Guid.Parse(userId));
            return Ok(result);
        }

        [HttpDelete("bookings/{id}")]
        public async Task<IActionResult> CancelBooking(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _enduserService.CancelBookingAsync(userId, id);
            return NoContent();
        }

        [HttpGet("payments")]
        public async Task<IActionResult> GetPayments()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _enduserService.GetPaymentsAsync(Guid.Parse(userId));
            return Ok(result);
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _enduserService.GetNotificationsAsync(int.Parse(userId));
            return Ok(result);
        }
    }
}
