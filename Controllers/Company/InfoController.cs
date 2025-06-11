using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Infrastructure.Services;
using Search2Go.Application.DTOs.Company;

namespace Search2Go.API.Controllers.Company
{
    [ApiController]
    [Route("api/company/Info")]
    [Authorize(Roles = "Company")]
    public class InfoController : ControllerBase
    {
        private readonly InfoService _infoService;

        public InfoController(InfoService infoService)
    {
        _infoService = infoService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<InfoDto>>> GetAll()
        {
            return Ok(await _infoService.GetAllAsync());
        }
    }
}