using NorthwindApp.Common;
using NorthwindApp.Domain;
using NorthwindApp.Models;

namespace NorthwindApp.Application;

public interface IUserService
{
    Task<Result<CreateUserResponse>> CreateUserAsync(CreateUserRequest request);
    Task<User> AddAsync(User user, string password);

    Task Update(User user, string password);

    Task<User?> GetById(int id);

    Task<User?> GetById(int id, string? includes = null);

    Task Delete(int id);

    Task<List<User>> Get(UserQueryObject query);

    Task<User?> GetByEmail(string email);
}