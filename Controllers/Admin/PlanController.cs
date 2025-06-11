using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Infrastructure.Services;  
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Plan")]
    [Authorize(Roles = "Admin")]
    public class PlanController : ControllerBase
    {
        private readonly PlanService _planService;

        public PlanController(PlanService planService)
    {
        _planService = planService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<PlanDto>>> GetAll()
        {
            return Ok(await _planService.GetAllAsync());
        }
    }
}