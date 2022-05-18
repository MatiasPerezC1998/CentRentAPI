using Microsoft.AspNetCore.Mvc;
using CentRent.Models;
using CentRent.Interfaces;

namespace CentRent.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController : ControllerBase
{
    public readonly ICarBusiness _carBusiness;

    public CarController(ICarBusiness carBusiness)
    {
        _carBusiness = carBusiness;
    }

    [HttpGet("GetAll")]
    public IEnumerable<CarResponse> GetAll()
    {
        return _carBusiness.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<CarResponse> Get(int id)
    {
        var car = _carBusiness.Get(id);

        if (car is not null)
        {
            return car;
            
        } else {

            return NotFound();

        }

    }

    [HttpGet("Registration/{registration}")]
    public ActionResult<CarResponse> GetCar(string registration)
    {
        var car = _carBusiness.GetCar(registration);

        if (car is not null)
        {
            return car;
        } else {
            return NotFound("No existe ningún coche con esa matrícula");
        }
    }

    [HttpPost("Create")]
    public ActionResult<CarResponse> Create([FromForm] CarRequest.CreateRequest car)
    {
        var newCar = _carBusiness.Add(car);
        return CreatedAtAction(nameof(Create), new { id = newCar.Id }, newCar);
    }

    [HttpPost("Update")]
    public ActionResult<CarResponse> Update([FromForm] CarRequest.UpdateRequest car)
    {
        var carUpdated = _carBusiness.Update(car);

        if (carUpdated != null)
        {
            return Ok(carUpdated);
        }

        return BadRequest("No existe el coche con el id " + car.Id);
    }

    [HttpPost("Delete")]
    public IActionResult Delete([FromForm] int id)
    {
        var car = _carBusiness.Get(id);

        if (car is null)
        {
            return NotFound();
        }

        _carBusiness.Delete(id);

        return Ok();
    }
}