using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NorthwindApp.Common;
using NorthwindApp.Domain;
using NorthwindApp.Infrastructure;
using NorthwindApp.Models;
using NorthwindApp.Models.Errors;

namespace NorthwindApp.Application;

public class AuthService : IAuthService
{
    private readonly NorthwindAppDbContext _dbContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthService(NorthwindAppDbContext dbContext,
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<Result<AuthUserResponse>> RegisterUserAsync(RegisterUserRequest request)
    {
        var newUser = new ApplicationUser()
        {
            UserName = request.Email,
            Email = request.Email,
        };

        var result = await _userManager.CreateAsync(newUser, request.Password);
        
        if (!result.Succeeded)
            return Result.Failure<AuthUserResponse>(AuthenticationErrors.IncorrectPassword);

        var newUserProfile = new UserProfile
        {
            UserId = newUser.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
        };
        
        _dbContext.UserProfiles.Add(newUserProfile);
        await _dbContext.SaveChangesAsync();
        
        await _userManager.AddClaimsAsync(newUser, [
            new Claim(ClaimTypes.GivenName, request.FirstName),
            new Claim(ClaimTypes.Surname, request.LastName),
            new Claim(ClaimTypes.NameIdentifier, request.Email),
            new Claim(ClaimTypes.Email, request.Email),
        ]);
        
        await _signInManager.SignInAsync(newUser, isPersistent: true);
        
        var userProfile = await _dbContext.UserProfiles
            .FirstAsync(x => x.UserId == newUser.Id);
        
        var userResponse = new AuthUserResponse(newUser.Id, newUser.Email, userProfile.FirstName, userProfile.LastName);

        return userResponse;
    }

    public async Task<Result<AuthUserResponse>> LoginUserAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        
        if (user is null)
            return Result.Failure<AuthUserResponse>(AuthenticationErrors.UserNotFound);
        
        var result = await _signInManager.PasswordSignInAsync(user, request.Password, true, false);
        
        if (!result.Succeeded)
            return Result.Failure<AuthUserResponse>(AuthenticationErrors.IncorrectPassword);
        
        var userProfile = await _dbContext.UserProfiles
            .FirstAsync(x => x.UserId == user.Id);
        
        var userResponse = new AuthUserResponse(user.Id, user.Email!, userProfile.FirstName, userProfile.LastName);

        return userResponse;
    }
}
