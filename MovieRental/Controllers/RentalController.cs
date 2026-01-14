using Microsoft.AspNetCore.Mvc;
using MovieRental.Rental;

namespace MovieRental.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RentalController : ControllerBase
    {
        private readonly IRentalFeatures _features;

        public RentalController(IRentalFeatures features)
        {
            _features = features;
        }

        [HttpPost]
        public IActionResult Post([FromBody] Rental.Rental rental)
        {
            return Ok(_features.SaveAsync(rental));
        }

        [HttpGet("GetRentalByCustomerName")]
        public async Task<IActionResult> GetRentalByCustomerName([FromQuery] string customerName)
        {
            var rentals = await _features.GetRentalsByCustomerNameAsync(customerName);

            return Ok(rentals);
        }
    }
}
