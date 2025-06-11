using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Agency;
using Search2Go.Application.Interfaces;
using Search2Go.Application.Services.Agency;
using System.Security.Claims;

namespace Search2Go.API.Controllers.Agency
{
    [ApiController]
    [Route("api/agency/manager")]
    [Authorize(Roles = "Agency")]
    public class AgencyManagerController : ControllerBase
    {
        private readonly IAgencyManagerService _service;

        public AgencyManagerController(IAgencyManagerService service)
        {
            _service = service;
        }

        private Guid GetAgencyId() => Guid.Parse(User.FindFirstValue("AgencyId")!);

        [HttpGet]
        public async Task<ActionResult<List<AgencyManagerDto>>> Get() =>
            Ok(await _service.GetAllAsync(GetAgencyId()));

        [HttpGet("{id}")]
        public async Task<ActionResult<AgencyManagerDto>> GetById(Guid id)
        {
            var result = await _service.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AgencyManagerDto>> Create(CreateAgencyManagerRequest request) =>
            Ok(await _service.CreateAsync(GetAgencyId(), request));

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateAgencyManagerRequest request)
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