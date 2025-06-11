using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Search2Go.Application.DTOs.MarketPlace;
using Search2Go.Application.Interfaces;
using Search2Go.Domain.Entities;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous] // or [Authorize(Roles = "Enduser")] if needed
    public class MarketplaceController : ControllerBase
    {
        private readonly IMarketplaceService _marketplaceService;

        public MarketplaceController(IMarketplaceService marketplaceService)
        {
            _marketplaceService = marketplaceService;
        }
        [HttpGet("available")]
        public async Task<ActionResult<List<MarketplaceItemDto>>> GetAvailable(Guid companyId)
        {
            return Ok(await _marketplaceService.GetAvailableItemsAsync(companyId));
        }

        [HttpPost("filter")]
        public async Task<ActionResult<List<MarketplaceItemDto>>> Filter([FromBody] MarketplaceFilterRequest request)
        {
            return Ok(await _marketplaceService.FilterItemsAsync(request));
        }
        [HttpPost("browse")]
        public async Task<IActionResult> Browse([FromBody] MarketplaceFilterRequest request)
        {
            var items = await _marketplaceService.BrowseAsync(request);
            return Ok(items);
        }

    }

}
