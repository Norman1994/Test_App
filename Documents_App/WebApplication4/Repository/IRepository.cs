using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication4.Entites;

namespace WebApplication4.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> FindAll();
        void Add(T item);
        void Remove(Guid id);
    }
}
