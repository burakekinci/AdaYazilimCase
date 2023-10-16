using Services.Abstract;

namespace Services.Concrete
{
    public class ServiceManager : IServiceManager
    {
        private readonly Lazy<IReservationService> _reservationServiceLazy;

        public ServiceManager()
        {
            _reservationServiceLazy = new Lazy<IReservationService>(() => new ReservationService());
        }

        public IReservationService ReservationService => _reservationServiceLazy.Value;


    }
}
