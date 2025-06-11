using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Company;
using Search2Go.Application.DTOs.User;
using Search2Go.Application.Interfaces;
using Search2Go.Infrastructure.Helpers;
using Search2Go.Infrastructure.Services;

[ApiController]
//[Route("api/[controller]")]
[Route("v1/company")]
[Authorize(Roles = "Admin,Agency,Company")]
public class CompanyController : ControllerBase
{
    private readonly ICompanyService _companyService;

    public CompanyController(ICompanyService companyService)
    {
        _companyService = companyService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var companies = await _companyService.GetAllCompaniesAsync();
        return Ok(companies);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var company = await _companyService.GetCompanyByIdAsync(id);
        return Ok(company);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCompanyRequest request)
    {
        var company = await _companyService.CreateCompanyAsync(request);
        return Ok(company);
    }
    [HttpGet("details")]
    public async Task<IActionResult> GetCompanyDetails()
    {
        Guid companyId = User.GetUserId(); // Make sure GetUserId() helper is available
        var result = await _companyService.GetCompanyDetailsAsync(companyId);
        return Ok(result);
    }
    [HttpPost("disable-user")]
    public async Task<IActionResult> DisableUser([FromBody] DisableUserRequest request)
    {
        Guid companyId = User.GetUserId();
        await _companyService.DisableUserAsync(int.Parse(companyId.ToString()), request.UserId);
        return Ok(new { success = true });
    }
    [HttpPost("enable-user")]
    public async Task<IActionResult> EnableUser([FromBody] EnableUserRequest request)
    {
        Guid companyId = User.GetUserId();
        await _companyService.EnableUserAsync(int.Parse(companyId.ToString()), request.UserId);
        return Ok(new { success = true });
    }
    [HttpGet("list")]
    public async Task<IActionResult> GetCompanyList([FromQuery] string? name = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var companies = await _companyService.GetCompaniesAsync(name ?? "", page, pageSize);
        return Ok(companies);
    }

    [HttpPost("extend-subscription")]
    public async Task<IActionResult> ExtendSubscription([FromBody] ExtendSubscriptionRequest request)
    {
        Guid companyId = User.GetUserId();
        await _companyService.ExtendSubscriptionAsync(companyId, request);
        return Ok(new { success = true });
    }
    [HttpPost("new")]
    [Authorize(Roles = "Admin,Agency")]
    public async Task<IActionResult> CreateCompany([FromBody] CreateCompanyRequest request)
    {
        var id = await _companyService.CreateCompanyAsync(request);
        return Ok(new { success = true, companyId = id });
    }

    [HttpPost("new-user")]
    [Authorize(Roles = "Company,Agency,Admin")]
    public async Task<IActionResult> CreateCompanyUser([FromBody] CreateCompanyUserRequest request)
    {
        var id = await _companyService.CreateCompanyUserAsync(request);
        return Ok(new { success = true, userId = id });
    }
    [HttpGet("require-nearby")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> GetNearbyResources()
    {
        Guid companyId = User.GetUserId();
        var result = await _companyService.GetNearbyResourcesAsync(companyId);
        return Ok(result);
    }
    [HttpPost("update-user")]
    [Authorize(Roles = "Company")]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
    {
        Guid companyId = User.GetUserId();
        await _companyService.UpdateUserAsync(companyId, request);
        return Ok(new { success = true });
    }

}
