using NorthwindApp.Common;
using NorthwindApp.Domain;
using NorthwindApp.Models;

namespace NorthwindApp.Application;

public interface IUserService
{
    Task<Result<CreateUserResponse>> CreateUserAsync(CreateUserRequest request);
    Task<Result<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequest request);
    Task<Result<UserResponse>> GetUserAsync(string id);
    Task<Result<UserResponse>> GetUserByEmailAsync(string email);
}