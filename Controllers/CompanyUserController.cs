using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Agency;
using Search2Go.Application.DTOs.Agency;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/agency/CompanyUser")]
    [Authorize(Roles = "Agency")]
    public class CompanyUserController : ControllerBase
    {
        private readonly CompanyUserService _companyUserService;

        public CompanyUserController(CompanyUserService companyUserService)
    {
        _companyUserService = companyUserService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<AgencyCompanyUserDto>>> GetAll()
        {
            return Ok(await _companyUserService.GetAllAsync());
        }
    }
}