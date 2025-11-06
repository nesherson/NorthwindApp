using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using NorthwindApp.Common;
using NorthwindApp.Models;

namespace NorthwindApp.Application;

public interface IAuthService
{
    Task<Result<AuthUserResponse>> RegisterUserAsync(RegisterUserRequest request);
    Task<Result<AuthUserResponse>> LoginUserAsync(LoginRequest request);
}
