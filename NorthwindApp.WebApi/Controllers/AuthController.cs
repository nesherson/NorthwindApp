using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;
using NorthwindApp.Common;
using NorthwindApp.Models;
using NorthwindApp.Models.Errors;

namespace NorthwindApp.WebApi;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IUserService _userService;

    public AuthController(IAuthService authService,  IUserService userService)
    {
        _authService = authService;
        _userService = userService;
    }
    
    [HttpPost("Register")]
    public async Task<IResult> Register(CreateUserRequest request)
    {
        var result = await _authService.RegisterUserAsync(request);

        if (result.IsFailure)
        {
            return ApiResults.Problem(Result.Failure(AuthenticationErrors.InvalidOrExpiredAuthenticationCode));
        }

        return result.Match(TypedResults.Ok, ApiResults.Problem);
    }

    [HttpPost("Login")]
    public async Task<IResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginUserAsync(request);
        
        return result.Match(Results.Ok, ApiResults.Problem);
    }
    
    [HttpPost("Logout")]
    [Authorize]
    public async Task<IResult> Logout()
    {
        var result = await _authService.LogoutUserAsync(User);
        
        if (result.IsFailure)
        {
            return ApiResults.Problem(Result.Failure(AuthenticationErrors.InvalidOrExpiredAuthenticationCode));
        }

        return result.Match(TypedResults.Ok, ApiResults.Problem);
    }
    
    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IResult> Refresh([FromBody] RefreshTokenRequest request)
    {
        var result = await _authService.RefreshTokenAsync(request);
        
        return result.Match(Results.Ok, ApiResults.Problem);
    }
    
    [HttpGet("Me")]
    [Authorize]
    public async Task<IResult> Me()
    {
        var emailClaim = (User.Identity as ClaimsIdentity)?
            .FindFirst(ClaimTypes.Email);

        if (emailClaim is null || string.IsNullOrEmpty(emailClaim.Value))
            return ApiResults.Problem(Result.Failure(AuthenticationErrors.ClaimNotFound));

        var result = await _userService.GetUserByEmailAsync(emailClaim.Value);
        
        if (result.IsFailure)
            return ApiResults.Problem(Result.Failure(AuthenticationErrors.UserNotFound));
        
        var response = new AuthUserResponse(result.Value.Id,
            result.Value.FirstName,
            result.Value.LastName,
            result.Value.Email);
        
        return result.Match(_ => Results.Ok(response), ApiResults.Problem);
    }
}
