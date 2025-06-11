using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.Services.Agency;
using Search2Go.Application.DTOs.Agency;

namespace Search2Go.API.Controllers.Agency
{
    [ApiController]
    [Route("api/agency/CreditCard")]
    [Authorize(Roles = "Agency")]
    public class CreditCardController : ControllerBase
    {
        private readonly CreditCardService _creditCardService;

        public CreditCardController(CreditCardService creditCardService)
    {
        _creditCardService = creditCardService;
    }

        [HttpGet("")]
        public async Task<ActionResult<List<CreditCardDto>>> GetAll()
        {
            return Ok(await _creditCardService.GetAllAsync());
        }
    }
}