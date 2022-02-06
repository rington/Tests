using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IReservationRepository<T> : IRepository<T> where T : class
    {
        Client GetClientByReservation(int reservationId);

        Room GetRoomByReservation(int reservationId);
    }
}
