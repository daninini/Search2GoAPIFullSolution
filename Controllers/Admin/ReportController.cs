using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Report")]
    [Authorize(Roles = "Admin")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportController(ReportService reportService)
    {
        _reportService = reportService;
    }

        [HttpGet("generate")]
        public async Task<ActionResult<ReportDto>> Generate()
        {
            return Ok(await _reportService.GenerateReportAsync());
        }
    }
}