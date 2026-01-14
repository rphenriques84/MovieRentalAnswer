using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MovieRental.Controllers
{
    [ApiController]
    public class ErrorController : Controller
    {
        [Route("error")]
        [HttpGet]
        public IActionResult Get()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

            // Optional: log exception here
            // _logger.LogError(exception, "Unhandled exception");

            return Problem(
                detail: exception?.Message,
                title: "An unexpected error occurred"
            );
        }
    }
}
