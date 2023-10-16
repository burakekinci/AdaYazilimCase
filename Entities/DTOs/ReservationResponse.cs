using Entities.Models;

namespace Entities.DTOs
{
    public class ReservationResponse
    {
        public bool RezervasyonYapilabilir { get; set; }
        public List<YerlesimAyrinti> YerlesimAyrinti { get; set; } = new List<YerlesimAyrinti>();
    }
}
