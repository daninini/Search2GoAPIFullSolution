using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Infrastructure.Services;

using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Subscription")]
    [Authorize(Roles = "Admin")]
    public class SubscriptionController : ControllerBase
    {
        private readonly SubscriptionService _subscriptionService;

        public SubscriptionController(SubscriptionService subscriptionService)
    {
        _subscriptionService = subscriptionService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<SubscriptionDto>>> GetAll()
        {
            return Ok(await _subscriptionService.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SubscriptionDto dto)
        {
            var result = await _subscriptionService.CreateSubscriptionAsync(dto);
            return Ok(result);
        }

        [HttpGet("{companyId}")]
        public async Task<IActionResult> GetCompanySubscriptions(Guid companyId)
        {
            var result = await _subscriptionService.GetCompanySubscriptionsAsync(companyId);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var result = await _subscriptionService.CancelSubscriptionAsync(id);
            return result ? Ok() : NotFound();
        }
    }
}