using System.Linq.Expressions;

namespace NorthwindApp.Application;

public interface IBaseRepository<T> where T : class
{
    Task<T?> GetById(int id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetList(Expression<Func<T, bool>> predicate);
    Task<int> GetCount();
    void Add(T entity);
    void Update(T entity);
    Task Delete(int id);
    Task SaveChanges();
    void Dispose();
}
