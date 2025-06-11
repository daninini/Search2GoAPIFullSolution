using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Menu")]
    [Authorize(Roles = "Admin")]
    public class MenuController : ControllerBase
    {
        private readonly MenuService _menuService;

        public MenuController(MenuService menuService)
    {
        _menuService = menuService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<MenuDto>>> GetAll()
        {
            return Ok(await _menuService.GetAllAsync());
        }
    }
}