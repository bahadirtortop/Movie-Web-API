using Microsoft.EntityFrameworkCore;
using Movie.Domain;
using Movie.Service.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Movie.Service.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public MovieContext _context { get; set; }

        public GenericRepository(MovieContext context)
        {
            _context = context;
        }

        public virtual List<T> GetAll()
        {
            return new List<T>(_context.Set<T>());
        }

        public virtual List<T> FindAllBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }


        public virtual T FindFirstBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public virtual T Find(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public virtual int Add(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _context.Set<T>().Add(entity);
            _context.Entry(entity).State = EntityState.Added;
            return _context.SaveChanges();
        }

        public virtual void Edit(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");

                _context.Set<T>().Update(entity);
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
            }

            catch (Exception dbEx)
            {
                throw new Exception("GenericRepository Edit", dbEx);
            }
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }

    }
}
