using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Infrastructure.Services;

using Search2Go.Application.DTOs.Manager;

namespace Search2Go.API.Controllers.Manager
{
    [ApiController]
    [Route("api/manager/Export")]
    [Authorize(Roles = "Manager")]
    public class ExportController : ControllerBase
    {
        private readonly ExportService _exportService;

        public ExportController(ExportService exportService)
    {
        _exportService = exportService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<ExportDto>>> GetAll()
        {
            return Ok(await _exportService.GetAllAsync());
        }
    }
}