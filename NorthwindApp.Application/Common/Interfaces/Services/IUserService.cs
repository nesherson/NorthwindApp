using NorthwindApp.Common;
using NorthwindApp.Domain;
using NorthwindApp.Models;

namespace NorthwindApp.Application;

public interface IUserService
{
    Task<Result<AuthUserResponse>> GetAuthUserAsync(int id);
}