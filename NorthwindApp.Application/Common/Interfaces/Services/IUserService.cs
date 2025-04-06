using NorthwindApp.Domain;

namespace NorthwindApp.Application;

public interface IUserService
{
    Task<User> Add(User user, string password);

    Task<User?> GetById(int id);

    Task<User?> GetById(int id, string? includes = null);
}