using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Admin;
using Search2Go.Application.Interfaces;
using Search2Go.Infrastructure.Helpers;
using Search2Go.Infrastructure.Services;


namespace Search2GoAPI.Controllers
{
    [ApiController]
    [Route("api/company-module/companies/{companyId}/subscription")]
    [Authorize(Roles = "Company,Admin")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _service;
        public SubscriptionController(ISubscriptionService service) => _service = service;

        [HttpPost("subscribe")]
        public async Task<ActionResult<SubscriptionDto>> Subscribe(Guid companyId, SubscribeRequest request)
        {
            var result = await _service.SubscribeAsync(companyId, request);
            return Ok(result);
        }

        [HttpGet("active")]
        public async Task<ActionResult<SubscriptionDto>> GetActive(Guid companyId)
        {
            var result = await _service.GetActiveSubscriptionAsync(companyId);
            return result == null ? NotFound() : Ok(result);
        }
        [HttpGet("subscription")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetSubscription()
        {
            Guid companyId = User.GetUserId();
            var result = await _service.GetSubscriptionAsync(companyId);
            return Ok(result);
        }
    }


}

