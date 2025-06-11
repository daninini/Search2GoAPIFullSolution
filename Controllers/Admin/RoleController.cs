using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Role")]
    [Authorize(Roles = "Admin")]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
    {
        _roleService = roleService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<RoleDto>>> GetAll()
        {
            return Ok(await _roleService.GetAllAsync());
        }
    }
}