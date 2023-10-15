using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace TrainReservation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(ILogger<ReservationController> logger)
        {
            _logger = logger;
        }

        [HttpPost(Name = "ReservationProcess")]
        public async Task<IActionResult> Post([FromBody] ReservationRequest reservationRequest)
        {

        }
    }
}