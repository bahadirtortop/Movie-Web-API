using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Movie.Service.Repository
{
    public interface IGenericRepository<T> : IDisposable where T : class
    {
        List<T> GetAll();
        List<T> FindAllBy(Expression<Func<T, bool>> predicate);
        T FindFirstBy(Expression<Func<T, bool>> predicate);
        T Find(int id);
        int Add(T entity);
        void Edit(T entity);
    }
}
