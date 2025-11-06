using Microsoft.EntityFrameworkCore;
using NorthwindApp.Common;
using NorthwindApp.Infrastructure;
using NorthwindApp.Models;
using NorthwindApp.Models.Errors;

namespace NorthwindApp.Application;

public class UserService : IUserService
{
    private readonly NorthwindAppDbContext _dbContext;

    public UserService(NorthwindAppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Result<AuthUserResponse>> GetAuthUserAsync(int id)
    {
        var user = await _dbContext.ApplicationUsers
            .Include(x => x.UserProfile)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
            return Result.Failure<AuthUserResponse>(UserErrors.UserNotFound(id));
        
        return new AuthUserResponse(user.Id, user.Email!, user.UserProfile.FirstName, user.UserProfile.LastName);
    }
}