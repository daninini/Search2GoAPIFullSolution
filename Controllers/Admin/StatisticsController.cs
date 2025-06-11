using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Statistics")]
    [Authorize(Roles = "Admin")]
    public class StatisticsController : ControllerBase
    {
        private readonly StatisticsService _statisticsService;

        public StatisticsController(StatisticsService statisticsService)
    {
        _statisticsService = statisticsService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<StatisticsDto>>> GetAll()
        {
            return Ok(await _statisticsService.GetAllAsync());
        }
    }
}