using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs;
using Search2Go.Application.Interfaces;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthService _authService;

        public UserController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var result = await _authService.RegisterAsync(request);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
