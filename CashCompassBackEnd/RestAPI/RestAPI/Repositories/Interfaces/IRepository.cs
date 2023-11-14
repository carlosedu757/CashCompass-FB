using System.Linq.Expressions;

namespace RestAPI.Repositories.Interfaces;

public interface IRepository<T>
{
    IQueryable<T> Get();
    IQueryable<T> GetAll();
    Task<T> GetById(Expression<Func<T, bool>> predicate);
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
