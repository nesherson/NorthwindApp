using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;
using NorthwindApp.Domain;
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

    [HttpPost]
    public async Task<ActionResult> Post(UserCreateRestModel model)
    {
        var newUser = new User();

        model.MapTo(newUser);
        var createdUser = await _userService.Add(newUser, model.Password);

        return CreatedAtAction(nameof(Post), new { id = createdUser.Id }, createdUser.MapToRestModel());
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, UserUpdateRestModel model)
    {
        var existingUser = await _userService.GetById(id);

        if (existingUser == null)
        {
            return NotFound();
        }

        model.MapTo(existingUser);

        try
        {
            await _userService.Update(existingUser, model.Password);
        }
        catch (Exception)
        {
            return BadRequest();
        }

        return NoContent();
    }
}