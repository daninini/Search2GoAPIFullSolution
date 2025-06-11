using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Infrastructure.Services;  
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/User")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
    {
        _userService = userService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<UserDto>>> GetAll()
        {
            return Ok(await _userService.GetAllAsync());
        }
    }
}