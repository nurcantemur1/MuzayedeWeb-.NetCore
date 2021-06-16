using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core
{
    public interface IEntityRepository<T> where T : class, new()
    {
        
        List<T> GetAll(Expression<Func<T, bool>> filter = null);

        T Get(Expression<Func<T, bool>> filter);

        T Add(T entity);

        T Update(T entity);

        bool Delete(T entity);
    }
}
