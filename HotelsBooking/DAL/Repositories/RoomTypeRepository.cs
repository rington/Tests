using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EFContext;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class RoomTypeRepository : IRepository<RoomType>
    {
        private readonly HotelContext _db;

        public RoomTypeRepository(HotelContext db)
        {
            _db = db;
        }
        public IEnumerable<RoomType> GetAll()
        {
            return _db.RoomTypes;
        }

        public RoomType Get(int id)
        {
            return _db.RoomTypes.Find(id);
        }

        public IEnumerable<RoomType> Find(Func<RoomType, bool> predicate)
        {
            return _db.RoomTypes.Where(predicate).ToList();
        }

        public void Create(RoomType roomType)
        {
            _db.RoomTypes.Add(roomType);
        }

        public void Update(RoomType roomType)
        {
            _db.Entry(roomType).State = EntityState.Modified;
        }

        public bool Delete(int id)
        {
            var roomType = _db.RoomTypes.Find(id);
            if (roomType != null)
            {
                _db.RoomTypes.Remove(roomType);
                return true;
            }

            return false;
        }
    }
}
