using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;
using NorthwindApp.Common;
using NorthwindApp.Domain;
using NorthwindApp.Models;
using NorthwindApp.Models.Errors;

namespace NorthwindApp.WebApi;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public AuthController(IAuthService authService,  
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _authService = authService;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    [HttpPost("Register")]
    public async Task<IResult> Register(RegisterUserRequest request)
    {
        var result = await _authService.RegisterUserAsync(request);

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpPost("Login")]
    public async Task<IResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginUserAsync(request);
        
        return result.Match(Results.Ok, ApiResults.Problem);
    }
    
    [HttpPost("Logout")]
    [Authorize]
    public IResult Logout()
    {
        _signInManager.SignOutAsync();
        
        return TypedResults.Ok();
    }
    
    [HttpGet("Me")]
    [Authorize]
    public async Task<IResult> Me()
    {
        var user = await _userManager.GetUserAsync(User);
        
        if (user is null)
            return ApiResults.Problem(Result.Failure(AuthenticationErrors.UserNotFound));
        
        var claims = await _userManager.GetClaimsAsync(user);
        
        var response = new AuthUserResponse(user.Id,
            user.Email!,
            claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? string.Empty,
            claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value ?? string.Empty);

        return Results.Ok(response);
    }
}
