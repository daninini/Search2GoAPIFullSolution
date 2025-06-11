using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Package")]
    [Authorize(Roles = "Admin")]
    public class PackageController : ControllerBase
    {
        private readonly PackageService _packageService;

        public PackageController(PackageService packageService)
    {
        _packageService = packageService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<PackageDto>>> GetAll()
        {
            return Ok(await _packageService.GetAllAsync());
        }
    }
}