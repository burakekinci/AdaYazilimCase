using Entities.DTOs;

namespace Services.Abstract
{
    public interface IReservationService
    {
        public ReservationResponse Reserve(ReservationRequest reservationRequest);
    }
}
