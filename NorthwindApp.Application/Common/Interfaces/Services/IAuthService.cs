using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using NorthwindApp.Common;
using NorthwindApp.Models;

namespace NorthwindApp.Application;

public interface IAuthService
{
    Task<Result> RegisterUserAsync(CreateUserRequest request);
    Task<Result<AuthResponse>> LoginUserAsync(LoginRequest request);
    Task<Result> LogoutUserAsync(ClaimsPrincipal user);
    Task<Result<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request);
}
