using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Agency")]
    [Authorize(Roles = "Admin")]
    public class AgencyController : ControllerBase
    {
        private readonly AgencyService _agencyService;

        public AgencyController(AgencyService agencyService)
    {
        _agencyService = agencyService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<AgencyDto>>> GetAll()
        {
            return Ok(await _agencyService.GetAllAsync());
        }
    }
}