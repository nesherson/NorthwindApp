using NorthwindApp.Domain;

namespace NorthwindApp.Application;

public interface IUserService
{
    Task Add(User user);
    Task<User?> GetById(int id);
}
