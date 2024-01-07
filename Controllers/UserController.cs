using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Picpay_01.Data;
using Picpay_01.Models;
using Picpay_01.Services;
using Picpay_01.ViewModels;

namespace Picpay_01.Controllers;

[ApiController]
//[Route("v1")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    private readonly IMemoryCache _cache;

    public UserController(
        UserService userService,
        IMemoryCache cache)
    {
        _userService = userService;
        _cache = cache;
    }
    
    
    [HttpPost("v1/users")]
    public async Task<IActionResult> CreateUserAsync(
        [FromBody] UserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        try
        {
            var newUser = await _userService.CreatedUserAsync(model);
            return Created($"v1/users/{newUser.Id}", newUser);
        }
        catch (DbException e)
        {
            return StatusCode(500, new ResulvViewModel<string>("Não foi possível criar a categoria"));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResulvViewModel<string>($"Falha interna no servidor - { e.Message }"));
        }
    }

    [HttpGet("v1/users")]
    public async Task<IActionResult> FindAllUsersAsync([FromServices] DataContext context)
    {
        try
        {
            var allUsers = await _cache.GetOrCreate("UsersCache", async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1);
                return await _userService.GetAllUsersAsync(context);
            });
            
            return Ok(new ResulvViewModel<List<Users>>(allUsers));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResulvViewModel<List<Users>>($"Falha interna no servidor - { e.Message }"));
        }
    }

    [HttpDelete("v1/users/{id:int}")]
    public async Task<IActionResult> DeleteUserByIdAsync(
        [FromRoute] int id)
    {
        try
        {
            var userById = await _userService.DeleteUserAsync(id);
            return Ok(new ResulvViewModel<Users>(userById));
        }
        catch (Exception e)
        {
            return StatusCode(500, new ResulvViewModel<Users>("Falha interna no servidor"));
        }
    }
}