using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Setting")]
    [Authorize(Roles = "Admin")]
    public class SettingController : ControllerBase
    {
        private readonly SettingService _settingService;

        public SettingController(SettingService settingService)
    {
        _settingService = settingService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<SettingDto>>> GetAll()
        {
            return Ok(await _settingService.GetAllAsync());
        }
    }
}