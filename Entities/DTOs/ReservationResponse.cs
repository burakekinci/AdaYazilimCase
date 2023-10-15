using Entities.Models;

namespace Entities.DTOs
{
    public class ReservationResponse
    {
        public bool RezervasyonYapilabilir { get; set; }
        public YerlesimAyrinti YerlesimAyrinti { get; set; }
    }
}
