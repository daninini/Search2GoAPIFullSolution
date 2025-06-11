using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Upgrade")]
    [Authorize(Roles = "Admin")]
    public class UpgradeController : ControllerBase
    {
        private readonly UpgradeService _upgradeService;

        public UpgradeController(UpgradeService upgradeService)
    {
        _upgradeService = upgradeService;
    }

        [HttpPost("upgrade-plan")]
        public async Task<ActionResult> UpgradePlan()
        {
            return Ok(await _upgradeService.UpgradePlanAsync());
        }
    }
}