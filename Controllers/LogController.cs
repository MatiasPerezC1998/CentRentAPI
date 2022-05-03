using Microsoft.AspNetCore.Mvc;
using CentRent.Helpers;
using CentRent.Models;
using CentRent.Services;

namespace CentRent.Controllers;

[ApiController]
[Route("[controller]")]
public class LogController : ControllerBase {
    ILogService _logService;

    public LogController(ILogService logService) {
        _logService = logService;
    }
    
    // LOGIN FUNCTIONS
    [Authorize]
    [HttpGet]
    public IActionResult GetAll() {
        var logs = _logService.GetAll();
        return Ok(logs);
    }
    
    // [HttpGet]
    // public IEnumerable<Log> GetAll() => _logService.GetAll();

    [HttpGet("{email}")]
    public ActionResult<Log> Get(string email) {
        var login = _logService.GetById(email);

        if (login == null) {
            return NotFound();
        }

        return login;
    }

    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model) {
        var response = _logService.Authenticate(model);

        if (response == null) {
            return BadRequest(new { message = "Usuario o contraseña incorrectos"});
        }

        return Ok(response);
    }

    // REGISTER FUNCTIONS
    // [HttpGet]
    // public IEnumerable<Log> GetAll() => _logService.GetAll();

    [HttpPost]
    public IActionResult Create(Log log) {
        _logService.Add(log);
        return CreatedAtAction(nameof(Create), new { email = log.Email }, log);
    }

}