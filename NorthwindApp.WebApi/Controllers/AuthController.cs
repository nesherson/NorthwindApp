using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;
using NorthwindApp.Common;
using NorthwindApp.Models;

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

    [HttpPost("login")]
    public async Task<IResult> Login(LoginRequest request)
    {
        var result = await _authService.AuthenticateUserAsync(request);
        
        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpPost("register")]
    public async Task<IResult> Register(CreateUserRequest request)
    {
        var result = await _userService.CreateUserAsync(request);

        return result.Match(Results.Ok, ApiResults.Problem);
    }

    [HttpGet("me")]
    public IActionResult Me()
    {
        return Ok(new UserReadRestModel()
        {
            Id = 1,
            FirstName = "Adam",
            LastName = "Testerson",
            Email = "adam.testerson@email.com",
            DateOfBirth = new DateTime(1990, 2, 2),
            DateCreated = DateTime.Now
        });
    }
}
