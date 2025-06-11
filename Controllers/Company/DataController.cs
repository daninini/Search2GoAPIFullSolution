using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Infrastructure.Services;
using Search2Go.Application.DTOs.Company;

namespace Search2Go.API.Controllers.Company
{
    [ApiController]
    [Route("api/company/Data")]
    [Authorize(Roles = "Company")]
    public class DataController : ControllerBase
    {
        private readonly DataService _dataService;

        public DataController(DataService dataService)
    {
        _dataService = dataService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<DataDto>>> GetAll()
        {
            return Ok(await _dataService.GetAllAsync());
        }
    }
}