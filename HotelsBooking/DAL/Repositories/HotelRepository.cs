using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EFContext;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class HotelRepository : IRepository<Hotel>
    {
        private readonly HotelContext _db;

        public HotelRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Hotel> GetAll()
        {
            return _db.Hotels;
        }

        public Hotel Get(int id)
        {
            return _db.Hotels.Find(id);
        }

        public IEnumerable<Hotel> Find(Func<Hotel, bool> predicate)
        {
            return _db.Hotels.Where(predicate).ToList();
        }

        public void Create(Hotel hotel)
        {
            _db.Hotels.Add(hotel);
        }

        public void Update(Hotel hotel)
        {
            _db.Entry(hotel).State = EntityState.Modified;
        }

        public bool Delete(int id)
        {
            var hotel = _db.Hotels.Find(id);
            if (hotel != null)
            {
                _db.Hotels.Remove(hotel);
                return true;
            }

            return false;
        }
    }
}
