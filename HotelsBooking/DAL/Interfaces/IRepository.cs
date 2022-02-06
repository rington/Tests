using System;
using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();

        IEnumerable<T> Find(Func<T, bool> predicate);

        T Get(int id);

        void Create(T item);

        void Update(T item);

        bool Delete(int id);
    }
}
