using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Interfaces;
using System.Security.Claims;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/{languageId}/{versionId}/info")]
    [Authorize(Roles = "Company,Manager,Admin")]
    public class CompanyInfoController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserContextService _userContextService;
        public CompanyInfoController(
          ICompanyService companyService,
          IHttpContextAccessor httpContextAccessor,
          IUserContextService userContextService)
        {
            _companyService = companyService;
            _httpContextAccessor = httpContextAccessor;
            _userContextService = userContextService;
        }

        [HttpGet("company-details")]
        public async Task<IActionResult> GetCompanyDetails()
        {
            var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrWhiteSpace(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return Unauthorized("User context not found.");

            var result = await _companyService.GetCompanyDetailsAsync(userId);
            if (result == null)
                return NotFound("Company not found");

            return Ok(result);
        }
        [HttpGet("has-advanced")]
        public async Task<IActionResult> HasAdvancedFeatures()
        {
            var companyId = _userContextService.GetCompanyId();
            if (companyId == null)
                return Unauthorized("Company context not found.");

            var result = await _companyService.HasAdvancedFeaturesAsync(companyId.Value);
            return Ok(result);
        }
    }
}
