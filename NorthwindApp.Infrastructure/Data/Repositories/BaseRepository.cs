using Microsoft.EntityFrameworkCore;
using NorthwindApp.Application;

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

    public async Task<IEnumerable<TEntity>> Get(IQueryable<TEntity> queryable, QueryObject query)
    {
        if (!string.IsNullOrEmpty(query.SortBy))
        {
            queryable = ExpressionBuilder.ApplyOrderBy(queryable, query.SortBy, query.SortOrder);
        }

        var skipNumber = (query.PageNumber - 1) * query.PageSize;

        return await queryable
            .Skip(skipNumber)
            .Take(query.PageSize)
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