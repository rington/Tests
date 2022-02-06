using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EFContext;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ReservationRepository : IReservationRepository<Reservation>
    {
        private readonly HotelContext _db;

        public ReservationRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Reservation> GetAll()
        {
            return _db.Reservations;
        }

        public Client GetClientByReservation(int reservationId)
        {
            return _db.Reservations.Include(r => r.Client).First(r => r.Id == reservationId).Client;
        }

        public Room GetRoomByReservation(int reservationId)
        {
            return _db.Reservations.Include(r => r.Room).First(r => r.Id == reservationId).Room;
        }

        public Reservation Get(int id)
        {
            return _db.Reservations.Find(id);
        }

        public IEnumerable<Reservation> Find(Func<Reservation, bool> predicate)
        {
            return _db.Reservations.Where(predicate).ToList();
        }
        
        public void Create(Reservation reservation)
        {
            _db.Reservations.Add(reservation);
        }

        public void Update(Reservation reservation)
        {
            _db.Entry(reservation).State = EntityState.Modified;
        }

        public bool Delete(int id)
        {
            var reservation = _db.Reservations.Find(id);
            if (reservation != null)
            {
                _db.Reservations.Remove(reservation);
                return true;
            }

            return false;
        }
    }
}
