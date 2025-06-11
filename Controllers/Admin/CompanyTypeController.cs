using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/CompanyType")]
    [Authorize(Roles = "Admin")]
    public class CompanyTypeController : ControllerBase
    {
        private readonly CompanyTypeService _companyTypeService;

        public CompanyTypeController(CompanyTypeService companyTypeService)
    {
        _companyTypeService = companyTypeService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<CompanyTypeDto>>> GetAll()
        {
            return Ok(await _companyTypeService.GetAllAsync());
        }
    }
}