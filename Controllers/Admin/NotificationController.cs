using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;
using Search2Go.Infrastructure.Services;
namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Notification")]
    [Authorize(Roles = "Admin")]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;

        public NotificationController(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<NotificationDto>>> GetAll()
        {
            return Ok(await _notificationService.GetAllAsync());
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserNotifications(int userId)
        {
            var result = await _notificationService.GetUserNotificationsAsync(userId);
            return Ok(result);
        }

        [HttpPost("mark-read/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(int notificationId)
        {
            var result = await _notificationService.MarkAsReadAsync(notificationId);
            return result ? Ok() : NotFound();
        }
    }
}