using NorthwindApp.Common;
using NorthwindApp.Models;

namespace NorthwindApp.Application;

public interface IAuthService
{
    Task<Result<CreateUserResponse>> RegisterUserAsync(CreateUserRequest request);
    Task<Result<LoginResponse>> AuthenticateUserAsync(LoginRequest request);

}
