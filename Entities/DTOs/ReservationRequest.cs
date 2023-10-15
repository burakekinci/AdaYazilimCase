using Entities.Models;

namespace Entities.DTOs
{
    public class ReservationRequest
    {
        public Tren Tren { get; set; }
        public int RezervasyonYapılacakKisiSayisi { get; set; }
        public bool KisilerFarkliVagonlaraYerlestirilebilir { get; set; }
    }
}
