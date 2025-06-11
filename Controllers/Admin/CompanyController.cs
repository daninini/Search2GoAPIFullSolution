using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Company;
using Search2Go.Application.Interfaces;

namespace Search2GoAPIFullSolution.Controllers.Admin
{
    [ApiController]
    [Route("api/companies")]
    [Authorize(Roles = "Admin,Agency")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _service;

        public CompanyController(ICompanyService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyDto>>> GetAll([FromQuery] Guid? agencyId) =>
            Ok(await _service.GetAllAsync(agencyId));

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyDto>> Create(CreateCompanyRequest request) =>
            Ok(await _service.CreateAsync(request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateCompanyRequest request)
        {
            var success = await _service.UpdateAsync(id, request);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
