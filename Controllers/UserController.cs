// UserController.cs
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.User;
using Search2Go.Application.Interfaces;
using Search2Go.Infrastructure.Helpers;
using Search2Go.Infrastructure.Services;
using System.Security.Claims;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/{languageId}/{versionId}/user")]
    [Authorize(Roles = "Company,Manager,Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(IUserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var profile = await _userService.GetProfileAsync(userId);
            return Ok(profile);
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile(UpdateUserProfileRequest request)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _userService.UpdateProfileAsync(userId, request);
            return NoContent();
        }

        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            await _userService.ChangePasswordAsync(userId, request);
            return NoContent();
        }
        [HttpPost("edit")]
        [Authorize(Roles = "User,Company,Seller")]
        public async Task<IActionResult> EditUser([FromBody] EditUserRequest request)
        {
            var result = await _userService.UpdateUserAsync(request);
            if (!result)
                return BadRequest("Failed to update user.");

            return Ok(new { status = true, message = "User updated successfully." });
        }
        [HttpPost("auth/generic-forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordRequest request)
        {
            var success = await _userService.ForgotPasswordAsync(request);
            if (!success) return NotFound(new { message = "Email not found" });
            return Ok(new { message = "Password reset link sent" });
        }

        [HttpPost("setuserinfo")]
        public async Task<IActionResult> SetUserInfo([FromBody] UpdateUserProfileRequest request)
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst("sub")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("Invalid user ID.");

            var result = await _userService.SaveUserProfileAsync(userId, request);
            if (!result) return NotFound("User not found.");

            return Ok(new { message = "User profile updated successfully" });
        }
        [HttpPost("profileimage")]
        [Authorize]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadProfileImage([FromForm] UploadProfileImageRequest request)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized();

            var result = await _userService.UploadProfileImageAsync(userId, request.ImageFile, "wwwroot");
            if (!result)
                return BadRequest("Image upload failed.");

            return Ok(new { message = "Image uploaded successfully." });
        }



    }
}
