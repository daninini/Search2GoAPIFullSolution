using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Company;
using Search2Go.Application.DTOs.Manager;
using Search2Go.Application.Interfaces;
using Search2Go.Infrastructure.Services;

namespace Search2Go.API.Controllers.Manager
{
    [ApiController]
    [Route("api/companies/{companyId}/users")]
    [Authorize(Roles = "Company,Admin")]
    public class CompanyUserController : ControllerBase
    {
        private readonly ICompanyUserService _service;

        public CompanyUserController(ICompanyUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<CompanyUserDto>>> Get(Guid companyId) =>
            Ok(await _service.GetAllAsync(companyId));

        [HttpGet("{id}")]
        public async Task<ActionResult<CompanyUserDto>> GetById(Guid companyId, Guid id)
        {
            var result = await _service.GetByIdAsync(companyId, id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<CompanyUserDto>> Create(Guid companyId, CreateCompanyUserRequest request) =>
            Ok(await _service.CreateAsync(companyId, request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid companyId, Guid id, UpdateCompanyUserRequest request)
        {
            var success = await _service.UpdateAsync(companyId, id, request);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid companyId, Guid id)
        {
            var success = await _service.DeleteAsync(companyId, id);
            return success ? NoContent() : NotFound();
        }
    }
}