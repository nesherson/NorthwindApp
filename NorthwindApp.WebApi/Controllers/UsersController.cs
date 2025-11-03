using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;
using NorthwindApp.Common;
using NorthwindApp.Models.Errors;

namespace NorthwindApp.WebApi;

[Route("api/[controller]")]
[Authorize]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IResult> Get(int id)
    {
        var result = await _userService.GetUserAsync("");

        if (result.IsFailure)
            return ApiResults.Problem(Result.Failure(AuthenticationErrors.UserNotFound));

        return result.Match(Results.Ok, ApiResults.Problem);
    }
}