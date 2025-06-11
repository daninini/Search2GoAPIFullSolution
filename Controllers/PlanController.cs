// PlanController.cs (API Controller)
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Admin;
using Search2Go.Application.DTOs.Plans;
using Search2Go.Application.Interfaces;
using Search2Go.Domain.Entities;

namespace Search2GoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PlanDto>>> GetAll() => Ok(await _planService.GetAllAsync());

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreatePlanRequest request)
        {
            var result = await _planService.CreatePlanAsync(request);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _planService.DeletePlanAsync(id);
            return NoContent();
        }
    }
}

