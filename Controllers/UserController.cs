using Microsoft.AspNetCore.Mvc;

using CentRent.Models;
using CentRent.Services;
using CentRent.Data;

namespace CentRent.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase {
    IUserService _service;

    public UserController(IUserService service) {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<User> GetAll() {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<User> Get(int id) {
        var user = _service.Get(id);

        if(user is not null) {
            return user;
        } else {
            return NotFound();
        }

    }

    [HttpPost]
    public IActionResult Create(User user) {            
        _service.Add(user);
        return CreatedAtAction(nameof(Create), new { id = user.Id }, user);
    }

    [HttpPost("{id}/update")]
    public IActionResult Update([FromRoute]int id, [FromForm]User user) {
        // if (id != user.Id) {
        //     return BadRequest();
        // }
            
        var existingUser = _service.Get(id);
        if(existingUser is null)
            return NotFound();
    
        _service.Update(user);           
    
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var user = _service.Get(id);
    
        if (user is null)
            return NotFound();
        
        _service.Delete(id);
    
        return NoContent();
    }
}