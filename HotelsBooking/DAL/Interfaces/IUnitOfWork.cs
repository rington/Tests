using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Room> Rooms { get; }

        IRepository<Hotel> Hotels { get; }

        IRepository<RoomType> RoomTypes { get; }

        IRepository<Client> Clients { get; }

        IReservationRepository<Reservation> Reservations { get; }

        void Save();
    }
}
