using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace TrainReservation.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ILogger<ReservationController> _logger;
        private readonly IServiceManager _serviceManager;

        public ReservationController(ILogger<ReservationController> logger, IServiceManager serviceManager)
        {
            _logger = logger;
            _serviceManager = serviceManager;
        }

        [HttpPost(Name = "ReservationProcess")]
        public IActionResult ReserveProcess([FromBody] ReservationRequest reservationRequest)
        {
            try
            {
                if (!ModelState.IsValid || reservationRequest is null)
                {
                    return BadRequest();
                }

                var response = _serviceManager.ReservationService.Reserve(reservationRequest);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}