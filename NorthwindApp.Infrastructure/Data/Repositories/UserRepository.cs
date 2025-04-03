using NorthwindApp.Application;
using NorthwindApp.Domain;

namespace NorthwindApp.Infrastructure;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(NorthwindAppDbContext dbContext) : base(dbContext)
    {
    }
}
