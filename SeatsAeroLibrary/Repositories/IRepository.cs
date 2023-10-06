using SeatsAeroLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeatsAeroLibrary.Repositories
{
    public interface IRepository<T,U>: IRepository, IKey<U,T>
    {
        IEnumerable<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(U id);
    }
    public interface IRepository
    {
        public void Initialize(IConfigSettings configSettings);
    }
}
