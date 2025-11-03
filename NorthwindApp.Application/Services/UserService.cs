using Microsoft.EntityFrameworkCore;
using NorthwindApp.Common;
using NorthwindApp.Domain;
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

    public async Task<Result<CreateUserResponse>> CreateUserAsync(CreateUserRequest request)
    {
        // var newUser = new User
        // {
        //     FirstName = request.FirstName,
        //     LastName = request.LastName,
        //     Email = request.Email,
        //     DateOfBirth = DateTime.MinValue,
        //     RoleId = 2,
        //     PasswordSalt = _passwordHasher.GenerateSalt()
        // };
        //
        // newUser.PasswordHash = _passwordHasher.ComputeHash(request.Password, newUser.PasswordSalt);
        //
        // // _dbContext.Users.Add(newUser);
        // await _dbContext.SaveChangesAsync();
        
        // return new CreateUserResponse(newUser.Id, newUser.FirstName, newUser.LastName, newUser.Email);
        return new CreateUserResponse(1, "", "", "");
    }
    
    public async Task<Result<UpdateUserResponse>> UpdateUserAsync(UpdateUserRequest request)
    {
        // var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
        //
        // if (user is null)
        //     return Result.Failure<UpdateUserResponse>(UserErrors.UserNotFound(request.Id));
        //
        // user.FirstName = request.FirstName;
        // user.LastName = request.LastName;
        // user.Email = request.Email;
        //
        // var newPasswordHash = _passwordHasher.ComputeHash(request.Password, user.PasswordSalt);
        //
        // if (newPasswordHash != user.PasswordHash)
        // {
        //     user.PasswordSalt = _passwordHasher.GenerateSalt();
        //     user.PasswordHash = _passwordHasher.ComputeHash(newPasswordHash, user.PasswordSalt);
        // }
        //
        // _dbContext.Users.Update(user);
        // await _dbContext.SaveChangesAsync();
        //
        // return new UpdateUserResponse(user.Id, user.FirstName, user.LastName, user.Email);
        return new UpdateUserResponse(1, "", "", "");
    }

    public async Task<Result<UserResponse>> GetUserAsync(string id)
    {
        return new UserResponse(1, "", "", "");
    }

    public async Task<Result<UserResponse>> GetUserByEmailAsync(string email)
    {
        // var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Email == email);
        //
        // if (user is null)
        //     return Result.Failure<UserResponse>(UserErrors.UserNotFound(email));
        //
        // return new UserResponse(user.Id, user.FirstName, user.LastName, user.Email);
        return new UserResponse(1, "", "", "");
    }
}