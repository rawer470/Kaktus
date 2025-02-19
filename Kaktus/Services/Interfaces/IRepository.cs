using System;
using System.Linq.Expressions;

namespace Kaktus.Services.Interfaces;

public interface IRepository<T> where T : class
{
    T Find(string id);

    IEnumerable<T> GetAll(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        string includeProperties = null,
        bool isTracking = true
    );

    T FirstOrDefault(
        Expression<Func<T, bool>> filter = null,
        string includeProperties = null,
        bool isTracking = true
    );

    void Add(T entity);
    void Remove(T entity);

    void RemoveRange(IEnumerable<T> entity);

    void Save();
}
