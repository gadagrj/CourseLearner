using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CoureLearner.WebApi
{
    public interface IRepository<T>
    {
        IEnumerable<T> Get(Expression<Func<T, bool>> predicate);
        T First(Expression<Func<T, bool>> predicate);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllOrderBy(Func<T, object> keySelector);
        IEnumerable<T> GetAllOrderByDescending(Func<T, object> keySelector);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Commit();
        void Dispose();
    }
}