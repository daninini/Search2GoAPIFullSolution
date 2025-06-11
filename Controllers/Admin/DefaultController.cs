using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Admin;
using Search2Go.Infrastructure.Services;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Default")]
    [Authorize(Roles = "Admin")]
    public class DefaultController : ControllerBase
    {
        private readonly DefaultService _defaultService;

        public DefaultController(DefaultService defaultService)
    {
        _defaultService = defaultService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<DefaultDto>>> GetAll()
        {
            return Ok(await _defaultService.GetAllAsync());
        }
    }
}