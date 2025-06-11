using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Admin;
using Search2Go.Application.DTOs.Plans;
using Search2Go.Application.Interfaces;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet("dashboard")]
        public async Task<ActionResult<DashboardStatsResponse>> GetDashboardStats()
        {
            return Ok(await _adminService.GetDashboardStatsAsync());
        }

        [HttpGet("users")]
        public async Task<ActionResult<List<UserSummaryResponse>>> GetAllUsers()
        {
            return Ok(await _adminService.GetAllUsersAsync());
        }

        [HttpPost("plans")]
        public async Task<ActionResult<PlanResponse>> CreatePlan(CreatePlanRequest request)
        {
            return Ok(await _adminService.CreatePlanAsync(request));
        }

        [HttpGet("plans")]
        public async Task<ActionResult<List<PlanResponse>>> GetAllPlans()
        {
            return Ok(await _adminService.GetAllPlansAsync());
        }
    }
}
