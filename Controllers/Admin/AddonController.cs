using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Addon")]
    [Authorize(Roles = "Admin")]
    public class AddonController : ControllerBase
    {
        private readonly AddonService _addonService;

        public AddonController(AddonService addonService)
    {
        _addonService = addonService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<AddonDto>>> GetAll()
        {
            return Ok(await _addonService.GetAllAsync());
        }
    }
}