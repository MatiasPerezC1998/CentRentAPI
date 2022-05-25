using CentRent.Models;
using CentRent.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IEnumerable<CarResponse>> GetAll()
    {
        return await _carBusiness.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CarResponse>> Get(int id)
    {
        var car = await _carBusiness.Get(id);

        if (car is not null)
        {
            return car;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpGet("Registration/{registration}")]
    public async Task<ActionResult<CarResponse>> GetCar(string registration)
    {
        var car = await _carBusiness.GetCar(registration);

        if (car is not null)
        {
            return car;
        }
        else
        {
            return NotFound("No existe ningún coche con esa matrícula");
        }
    }

    [HttpGet("AvailableCars")]
    public async Task<ActionResult> AvailableCars()
    {
        var cars = await _carBusiness.GetAvailableCarsFromType();
        return Ok(cars);
    }

    [HttpPost("Create")]
    public async Task<ActionResult<CarResponse>> Create([FromForm] CarRequest.CreateRequest car)
    {
        var newCar = await _carBusiness.Add(car);
        return Ok(newCar);
    }


    [HttpPost("Update")]
    public async Task<ActionResult<CarResponse>> Update([FromForm] CarRequest.UpdateRequest car)
    {
        var carUpdated = await _carBusiness.Update(car);

        if (carUpdated != null)
        {
            return Ok(carUpdated);
        }

        return BadRequest("No existe el coche con el id " + car.Id);
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete([FromForm] int id)
    {
        var car = await _carBusiness.Get(id);

        if (car is null)
        {
            return NotFound();
        }

        await _carBusiness.Delete(id);

        return Ok();
    }

    [HttpGet("GetImage")]
    public ActionResult GetImage(String imageUrl)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "Images", imageUrl);
        var image = System.IO.File.OpenRead(path);

        return File(image, "image/jpg");
    }
}