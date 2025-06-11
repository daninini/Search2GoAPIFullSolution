using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Seller;
using Search2Go.Application.Interfaces;
using Search2Go.Domain.Entities;
using System.Security.Claims;

namespace Search2GoAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin,Agency,Company")]
    public class SellerController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellerController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //{
        //    var sellers = await _sellerService.GetAllAsync();
        //    return Ok(sellers);
        //}

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSellerRequest request)
        {
            var seller = await _sellerService.CreateAsync(request);
            return Ok(seller);
        }
        private Guid GetAgencyId() => Guid.Parse(User.FindFirstValue("AgencyId")!);

        [HttpGet]
        public async Task<ActionResult<List<SellerDto>>> Get() =>
            Ok(await _sellerService.GetAllAsync(GetAgencyId()));

        [HttpGet("{id}")]
        public async Task<ActionResult<SellerDto>> GetById(Guid id)
        {
            var result = await _sellerService.GetByIdAsync(id);
            return result == null ? NotFound() : Ok(result);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateSellerRequest request)
        {
            var success = await _sellerService.UpdateAsync(id, request);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _sellerService.DeleteAsync(id);
            return success ? NoContent() : NotFound();
        }
    }

}
