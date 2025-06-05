using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
namespace Search2GoAPIFullSolution.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // 👈 Requires a valid JWT token
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _service;
    public CompanyController(ICompanyService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _service.GetAllAsync();
        return Ok(result);
    }
}
