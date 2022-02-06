using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EFContext;
using DAL.Entities;
using DAL.Interfaces;


namespace DAL.Repositories
{
    public class ClientRepository : IRepository<Client>
    {
        private readonly HotelContext _db;

        public ClientRepository(HotelContext db)
        {
            _db = db;
        }

        public IEnumerable<Client> GetAll()
        {
            return _db.Clients;
        }

        public Client Get(int id)
        {
            return _db.Clients.Find(id);
        }

        public IEnumerable<Client> Find(Func<Client, bool> predicate)
        {
            return _db.Clients.Where(predicate).ToList();
        }

        public void Create(Client client)
        {
            _db.Clients.Add(client);
        }

        public void Update(Client client)
        {
            _db.Entry(client).State = EntityState.Modified;
        }

        public bool Delete(int id)
        {
            var client = _db.Clients.Find(id);
            if (client != null)
            {
                _db.Clients.Remove(client);
                return true;
            }

            return false;
        }
    }
}
