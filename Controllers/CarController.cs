using Microsoft.AspNetCore.Mvc;

using CentRent.Models;
using CentRent.Services;

namespace CentRent.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase {
    ICarService _service;

    public CarController(ICarService service) {
        _service = service;
    }

    [HttpGet("GetAll")]
    public IEnumerable<Car> GetAll() {
        return _service.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Car> Get(int id) {
        var car = _service.Get(id);

        if(car is not null) {
            return car;
        } else {
            return NotFound();
        }
        
    }

    [HttpPost]
    public IActionResult Create(Car car) {            
        _service.Add(car);
        return CreatedAtAction(nameof(Create), new { id = car.Id }, car);
    }

    [HttpPost("{id}/update")]
    public IActionResult Update([FromRoute]int id, [FromForm]Car car) {
        // if (id != car.Id) {
        //     return BadRequest();
        // }
            
        var existingCar = _service.Get(id);
        if(existingCar is null)
            return NotFound();
    
        _service.Update(car);

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id) {
        var car = _service.Get(id);
    
        if (car is null)
            return NotFound();
        
        _service.Delete(id);
    
        return NoContent();
    }
}