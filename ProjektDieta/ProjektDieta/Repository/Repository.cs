using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjektDieta.Db;
using ProjektDieta.Models;

namespace ProjektDieta.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IModel
    {
        private readonly AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Delete(T model)
        {
            _context.Set<T>().Remove(model);
            _context.SaveChanges();
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetById(long id)
        {
            return _context.Set<T>().FirstOrDefault(model => id == model.Id);
        }

        public T GetById(long id, params Expression<Func<T, object>>[] properties)
        {
            var query = _context.Set<T>().AsQueryable();
            query = properties.Aggregate(query, (current, property) => current.Include(property));

            return query.FirstOrDefault(model => id == model.Id);
        }

        public long Insert(T model)
        {
            _context.Set<T>().Add(model);
            _context.SaveChanges();
            return model.Id;
        }

        public void Update(T model)
        {
            _context.SaveChanges();
        }
    }
}
