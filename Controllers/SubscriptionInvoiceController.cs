using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Subscriptions;
using Search2Go.Application.Interfaces;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/company-module/companies/{companyId}/invoices")]
    [Authorize(Roles = "Company,Manager,Admin")]
    public class SubscriptionInvoiceController : ControllerBase
    {
        private readonly ISubscriptionInvoiceService _invoiceService;

        public SubscriptionInvoiceController(ISubscriptionInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubscriptionInvoiceResponse>>> GetInvoices(Guid companyId)
        {
            return Ok(await _invoiceService.GetInvoicesByCompanyIdAsync(companyId));
        }
    }
}
