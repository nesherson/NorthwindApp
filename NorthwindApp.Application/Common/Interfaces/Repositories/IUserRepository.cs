using NorthwindApp.Domain;

namespace NorthwindApp.Application;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetById(int id, string? includes = null);

    Task<User?> GetByEmail(string email);

    Task<List<User>> Get(UserQueryObject query);
}