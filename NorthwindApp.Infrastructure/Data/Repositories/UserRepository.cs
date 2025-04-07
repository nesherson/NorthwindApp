using Microsoft.EntityFrameworkCore;
using NorthwindApp.Application;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(NorthwindAppDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<User?> GetById(int id, string? includes = null)
    {
        List<string> incl = new();

        if (!string.IsNullOrEmpty(includes))
        {
            incl = includes.Split(',').ToList();
        }

        var queryable = _dbContext.Set<User>().AsQueryable();

        foreach (var include in incl)
        {
            queryable = queryable.Include(include);
        }

        return await queryable.FirstOrDefaultAsync(x => x.Id == id);
    }
}