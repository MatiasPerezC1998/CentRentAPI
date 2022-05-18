using Microsoft.EntityFrameworkCore;
using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Data;

public class CarRepository : ICarRepository
{
    private readonly CentRentContext _context;
    public CarRepository(CentRentContext context)
    {
        _context = context;
    }

    public IEnumerable<CarResponse> GetAll()
    {
        return _context.Cars
            .Select(p => new CarResponse(p))
            .AsNoTracking()
            .ToList();
    }

    public CarResponse? Get(int id)
    {
        var car = _context.Cars
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);

        return new CarResponse(car);
    }

    public CarResponse Add(CarRequest.CreateRequest newCar)
    {
        var car = new Car(newCar);

        _context.Cars.Add(car);
        _context.SaveChanges();

        return new CarResponse(car);
    }

    public void Delete(Car car)
    {
        _context.Cars.Remove(car);
        _context.SaveChanges();
    }


    public CarResponse Update(Car car)
    {
        _context.Cars.Update(car);
        _context.SaveChanges();

        return new CarResponse(car);
    }
}