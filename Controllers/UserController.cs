using Microsoft.AspNetCore.Mvc;
using CentRent.Helpers;
using CentRent.Entities;
using CentRent.Models;
using CentRent.Services;
using CentRent.Interfaces;

namespace CentRent.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public readonly IUserBusiness _userBusiness;

    public UserController(IUserBusiness userBusiness)
    {
        _userBusiness = userBusiness;
    }

    [Authorize]
    [HttpGet("Authorization")]
    public IActionResult GetAll()
    {
        var users = _userBusiness.GetAll();
        return Ok(users);
    }

    [HttpGet("{email}")]
    public ActionResult<UserResponse> Get(string email)
    {
        var login = _userBusiness.GetById(email);

        if (login == null)
        {
            return NotFound();
        }

        return login;
    }

    [HttpPost("Login")]
    public ActionResult<LoginResponse> Login([FromForm] UserRequest.LoginRequest model)
    {
        var response = _userBusiness.Login(model);

        if (response == null)
        {
            return BadRequest(new { message = "Usuario o contraseña incorrectos" });
        }

        return Ok(response);
    }

    // [HttpGet]
    // public IEnumerable<Log> GetAll() => _logService.GetAll();

    [HttpPost("Register")]
    public ActionResult<UserResponse> Register([FromForm] UserRequest.RegisterRequest user)
    {
        var newUser = _userBusiness.Register(user);
        return CreatedAtAction(nameof(Register), new { email = newUser.Email }, newUser);
    }

    [HttpDelete("{email}")]
    public IActionResult Delete(string email)
    {
        var user = _userBusiness.Get(email);

        if (user is null)
        {
            return NotFound();
        }

        _userBusiness.Delete(email);

        return NoContent();
    }

}