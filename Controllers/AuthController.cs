using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Auth;
using Search2Go.Application.Interfaces;
using Search2Go.Infrastructure.Services;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var token = await _auth.RegisterAsync(request);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var token = await _auth.LoginAsync(request);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { error = ex.Message });
            }
        }
        [HttpPost("generic-forgot-password")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordRequest request)
        {
            if (string.IsNullOrEmpty(request.Email))
                return BadRequest("Email is required.");

            var result = await _auth.SendPasswordResetAsync(request.Email);
            if (!result) return NotFound("User with this email not found.");

            return Ok(new { message = "Password reset link sent." });
        }
    }
}
