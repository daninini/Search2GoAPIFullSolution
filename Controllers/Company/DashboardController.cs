using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Infrastructure.Services;
using Search2Go.Application.DTOs.Company;

namespace Search2Go.API.Controllers.Company
{
    [ApiController]
    [Route("api/company/Dashboard")]
    [Authorize(Roles = "Company")]
    public class DashboardController : ControllerBase
    {
        private readonly DashboardService _dashboardService;

        public DashboardController(DashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<DashboardDto>>> GetAll()
        {
            return Ok(await _dashboardService.GetAllAsync());
        }
    }
}