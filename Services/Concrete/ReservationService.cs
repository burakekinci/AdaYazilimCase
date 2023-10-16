using Entities.DTOs;
using Entities.Models;
using Services.Abstract;

namespace Services
{
    public class ReservationService : IReservationService
    {
        public ReservationService()
        {

        }

        public ReservationResponse Reserve(ReservationRequest reservationRequest)
        {
            ReservationResponse reservationResponse = new ReservationResponse();
            //Sadece bir vagondan işlem yapılabilir
            if (!reservationRequest.KisilerFarkliVagonlaraYerlestirilebilir)
            {
                foreach (var item in reservationRequest.Tren.Vagonlar)
                {
                    if (IsWagonCompatibleForSingleUse(item, reservationRequest.RezervasyonYapılacakKisiSayisi))
                    {
                        reservationResponse.YerlesimAyrinti.Add(new YerlesimAyrinti { VagonAdi = item.Ad, KisiSayisi = reservationRequest.RezervasyonYapılacakKisiSayisi });
                        reservationResponse.RezervasyonYapilabilir = true;
                        break;
                    }
                }
            }
            else //Diğer vagonlar da kullanılabilir
            {
                int reservationNumberLeft = reservationRequest.RezervasyonYapılacakKisiSayisi;
                int usedReservationNumber = 0;
                foreach (var item in reservationRequest.Tren.Vagonlar)
                {
                    if (reservationNumberLeft > 0)
                    {
                        WagonCompatibleProcessForMultiUse(item, ref reservationNumberLeft, out usedReservationNumber);
                        if (usedReservationNumber > 0)
                        {
                            reservationResponse.YerlesimAyrinti.Add(new YerlesimAyrinti { VagonAdi = item.Ad, KisiSayisi = usedReservationNumber });
                        }
                    }
                }
                reservationResponse.RezervasyonYapilabilir = reservationNumberLeft == 0;
            }

            if (!reservationResponse.RezervasyonYapilabilir)
                reservationResponse.YerlesimAyrinti.Clear();

            return reservationResponse;

        }

        private int MaxAllowedSeatNumber(int capacity) => (capacity * 70) / 100;

        //Tek bir vagona rezervasyon yapacak kişi sayısı yetiyor mu onun kontrolü yapılıyor
        private bool IsWagonCompatibleForSingleUse(Vagon vagon, int personReservationNumber)
        {
            if ((MaxAllowedSeatNumber(vagon.Kapasite) - vagon.DoluKoltukAdet) >= personReservationNumber)
                return true;
            return false;
        }

        //Rezervasyon yapcak kişi sayısı her bir vagon kullanımında azaltılıyor ve vagon kullanımı başına kaç rezervasyon yapıldığı out olarak veriliyor.
        private void WagonCompatibleProcessForMultiUse(Vagon vagon, ref int personReservationNumberLeft, out int _usedReservationNumber)
        {
            int maxNewPersonReservationNumber = (MaxAllowedSeatNumber(vagon.Kapasite) - vagon.DoluKoltukAdet);
            if (maxNewPersonReservationNumber <= personReservationNumberLeft)
            {
                personReservationNumberLeft -= maxNewPersonReservationNumber;
                _usedReservationNumber = maxNewPersonReservationNumber;
            }
            else
            {
                _usedReservationNumber = personReservationNumberLeft;
                personReservationNumberLeft = 0;
            }
        }
    }
}
