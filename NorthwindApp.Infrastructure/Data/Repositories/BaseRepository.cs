using Microsoft.EntityFrameworkCore;
using NorthwindApp.Application;
using System.Linq.Expressions;

namespace NorthwindApp.Infrastructure;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly NorthwindAppDbContext _dbContext;

    public BaseRepository(NorthwindAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Add(TEntity entity)
    {
        _dbContext.Set<TEntity>().Add(entity);
        _dbContext.SaveChanges();
    }

    public async Task Delete(int id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);

        if (entity == null)
            return;

        _dbContext.Remove(entity);
    }

    public void Dispose()
    {
        _dbContext?.Dispose();
    }

    public async Task<IEnumerable<TEntity>> GetAll()
    {
        return await _dbContext.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> GetById(int id)
    {
        return await _dbContext.Set<TEntity>().FindAsync(id);
    }

    public Task<int> GetCount()
    {
        return _dbContext.Set<TEntity>().CountAsync();
    }

    public async Task<IEnumerable<TEntity>> GetList(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbContext.Set<TEntity>()
            .Where(predicate)
            .ToListAsync();
    }

    public void Update(TEntity entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public async Task SaveChanges()
    {
        await _dbContext.SaveChangesAsync();
    }
}
