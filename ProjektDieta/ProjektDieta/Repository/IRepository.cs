using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ProjektDieta.Models;

namespace ProjektDieta.Repository
{
    public interface IRepository<T> where T : IModel
    {
        IEnumerable<T> GetAll();
        T GetById(long id);
        T GetById(long id, params Expression<Func<T, object>>[] properties);
        long Insert(T model);
        void Update(T model);
        void Delete(T model);
    }
}
