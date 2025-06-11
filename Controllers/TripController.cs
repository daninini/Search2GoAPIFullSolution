using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Search2Go.Application.DTOs.Trip;
using Search2Go.Application.Interfaces;

namespace Search2GoAPIFullSolution.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Seller,Company,Admin,Enduser")]
    public class TripController : ControllerBase
    {
        private readonly ITripService _tripService;

        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trips = await _tripService.GetAllAsync();
            return Ok(trips);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var trip = await _tripService.GetByIdAsync(id);
            return Ok(trip);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTripRequest request)
        {
            var trip = await _tripService.CreateAsync(request);
            return Ok(trip);
        }
    }

}
