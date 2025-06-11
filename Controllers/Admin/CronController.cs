using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;
using Search2Go.Application.Interfaces;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Cron")]
    [Authorize(Roles = "Admin")]
    public class CronController : ControllerBase
    {
        private readonly CronService _cronService;

        public CronController(CronService cronService)
    {
        _cronService = cronService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<CronDto>>> GetAll()
        {
            return Ok(await _cronService.GetAllAsync());
        }
    }
}