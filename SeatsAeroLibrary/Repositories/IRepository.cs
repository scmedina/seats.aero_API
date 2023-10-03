using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public interface IRepository<T,U>
    {
        IEnumerable<T> GetAll();
        T GetById(U id);
        void Add(T entity);
        void Update(T entity);
        void Delete(U id);
    }
}
