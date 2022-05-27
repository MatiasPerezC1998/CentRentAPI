using Microsoft.AspNetCore.Mvc;
using CentRent.Models;
using CentRent.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace CentRent.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public readonly IUserBusiness _userBusiness;

    public UserController(IUserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    [HttpGet("Authorization")]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userBusiness.GetAll();
        return Ok(users);
    }

    [HttpGet("{email}")]
    public async Task<ActionResult<UserResponse>> Get(string email)
    {
        var login = await _userBusiness.GetByEmail(email);

        if (login == null)
        {
            return NotFound();
        }

        return login;
    }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login([FromForm] UserRequest.LoginRequest model)
    {
        var response = await _userBusiness.Login(model);

        if (response == null)
        {
            return BadRequest(new { message = "Usuario o contraseña incorrectos" });
        }

        return Ok(response);
    }

    // [HttpGet]
    // public IEnumerable<Log> GetAll() => _logService.GetAll();

    [HttpPost("Register")]
    [AllowAnonymous]
    public async Task<ActionResult<UserResponse>> Register([FromForm] UserRequest.RegisterRequest user)
    {
        var newUser = await _userBusiness.Register(user);
        return Ok(newUser);
    }

    [HttpDelete("{email}")]
    public async Task<IActionResult> Delete(string email)
    {
        var user = await _userBusiness.Get(email);

        if (user is null)
        {
            return NotFound();
        }

        await _userBusiness.Delete(email);

        return NoContent();
    }

}