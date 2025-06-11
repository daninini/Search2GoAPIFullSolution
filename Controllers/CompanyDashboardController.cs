using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Interfaces;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/companies/{companyId}/dashboard")]
    [Authorize(Roles = "Company,Admin")]
    public class CompanyDashboardController : ControllerBase
    {
        private readonly ICompanyDashboardService _service;

        public CompanyDashboardController(ICompanyDashboardService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetDashboard(Guid companyId)
        {
            var result = await _service.GetDashboardAsync(companyId);
            return Ok(result);
        }
    }
}
