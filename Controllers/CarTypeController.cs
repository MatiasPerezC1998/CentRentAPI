using CentRent.Models;
using CentRent.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CentRent.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CarTypeController : ControllerBase
{
    public readonly ICarTypeBusiness _carTypeBusiness;

    public CarTypeController(ICarTypeBusiness carTypeBusiness)
    {
        _carTypeBusiness = carTypeBusiness;
    }

    [HttpGet("GetAll")]
    public async Task<IEnumerable<CarTypeResponse>> GetAll()
    {
        return await _carTypeBusiness.GetAll();
    }

    [HttpGet("GetById")]
    public async Task<ActionResult<CarTypeResponse>> Get(int id)
    {
        var carType = await _carTypeBusiness.Get(id);

        if (carType is not null)
        {
            return carType;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<CarTypeResponse>> Create([FromForm] CarTypeRequest.CreateRequest carType)
    {
        var newCarType = await _carTypeBusiness.Add(carType);
        return Ok(newCarType);
    }


    [HttpPost("Update")]
    public async Task<ActionResult<CarTypeResponse>> Update([FromForm] CarTypeRequest.UpdateRequest carType)
    {
        var carTypeUpdated = await _carTypeBusiness.Update(carType);

        if (carTypeUpdated != null)
        {
            return Ok(carTypeUpdated);
        }

        return BadRequest("No existe el coche con el id " + carType.Id);
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete([FromForm] int id)
    {
        var car = await _carTypeBusiness.Get(id);

        if (car is null)
        {
            return NotFound();
        }

        await _carTypeBusiness.Delete(id);

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