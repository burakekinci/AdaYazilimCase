namespace Services.Abstract
{
    public interface IServiceManager
    {
        IReservationService ReservationService { get; }
    }
}
