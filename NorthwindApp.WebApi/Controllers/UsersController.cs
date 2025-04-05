using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;
using NorthwindApp.WebApi.Extensions;

namespace NorthwindApp.WebApi;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserReadRestModel>> Get(int id)
    {
        var entity = await _userService.GetById(id, "Role");
        var model = new UserReadRestModel();

        entity.MapTo(model);

        return model;
    }
}