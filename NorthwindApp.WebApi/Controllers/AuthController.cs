using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;

namespace NorthwindApp.WebApi;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRestModel model)
    {
        var result = await _authService.LogInUser(model.Email, model.Password);

        if (!result.IsSuccess)
            return Unauthorized(result.Message);

        Response.Headers.Append("Authorization", result.Token);

        return Ok(result);
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
