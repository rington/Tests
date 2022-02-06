using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EFContext;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class RoomRepository : IRepository<Room>
    {
        private readonly HotelContext _db;

        public RoomRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Room> GetAll()
        {
            return _db.Rooms;
        }

        public Room Get(int id)
        {
            return _db.Rooms.Find(id);
        }

        public void Create(Room room)
        {
            _db.Rooms.Add(room);
        }

        public void Update(Room room)
        {
            _db.Entry(room).State = EntityState.Modified;
        }

        public IEnumerable<Room> Find(Func<Room, bool> predicate)
        {
            return _db.Rooms.Where(predicate).ToList();
        }

        public bool Delete(int id)
        {
            var room = _db.Rooms.Find(id);

            if (room != null)
            {
                _db.Rooms.Remove(room);
                return true;
            }

            return false;
        }
    }
}
