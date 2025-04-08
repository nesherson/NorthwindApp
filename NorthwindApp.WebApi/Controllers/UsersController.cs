using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NorthwindApp.Application;
using NorthwindApp.Domain;
using NorthwindApp.WebApi.Extensions;

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
    public async Task<ActionResult<UserReadRestModel>> Get(int id)
    {
        var user = await _userService.GetById(id, "Role");

        if (user == null)
        {
            return NotFound();
        }

        var model = new UserReadRestModel();

        user.MapTo(model);

        return model;
    }

    [HttpGet]
    public async Task<ActionResult> Get([FromQuery] UserQueryObject query)
    {
        var result = await _userService.Get(query);

        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult> Post(UserCreateRestModel model)
    {
        var newUser = new User();

        model.MapTo(newUser);

        try
        {
            var createdUser = await _userService.Add(newUser, model.Password);

            return CreatedAtAction(nameof(Post), new { id = createdUser.Id }, createdUser.MapToRestModel());
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, UserUpdateRestModel model)
    {
        var userToUpdate = await _userService.GetById(id);

        if (userToUpdate == null)
        {
            return NotFound();
        }

        model.MapTo(userToUpdate);

        try
        {
            await _userService.Update(userToUpdate, model.Password);

            return NoContent();
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var userToDelete = await _userService.GetById(id);

        if (userToDelete == null)
        {
            return NotFound();
        }

        try
        {
            await _userService.Delete(id);

            return NoContent();

        }
        catch (Exception)
        {
            return BadRequest();
        }
    }
}