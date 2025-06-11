using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/ResendEmail")]
    [Authorize(Roles = "Admin")]
    public class ResendEmailController : ControllerBase
    {
        private readonly ResendEmailService _resendEmailService;

        public ResendEmailController(ResendEmailService resendEmailService)
    {
        _resendEmailService = resendEmailService;
    }

        [HttpPost("resend")]
        public async Task<ActionResult> Resend()
        {
            return Ok(await _resendEmailService.ResendAsync());
        }
    }
}