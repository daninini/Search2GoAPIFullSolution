using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Admin;
using Search2Go.Application.DTOs.Admin;

namespace Search2Go.API.Controllers.Admin
{
    [ApiController]
    [Route("api/admin/Transaction")]
    [Authorize(Roles = "Admin")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
    {
        _transactionService = transactionService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<TransactionDto>>> GetAll()
        {
            return Ok(await _transactionService.GetAllAsync());
        }
    }
}