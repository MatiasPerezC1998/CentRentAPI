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
            .Include(x => x.CarType)
            .Select(p => new CarResponse(p))
            .AsNoTracking()
            .ToList();
    }

    public CarResponse? Get(int id)
    {
        var car = _context.Cars
            .Include(x => x.CarType)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);

        return new CarResponse(car);
    }

    public CarResponse? GetCar(string registration)
    {
        var car = _context.Cars
            .Include(x => x.CarType)
            .AsNoTracking()
            .SingleOrDefault(p => p.Registration == registration);

        if (car != null)
        {
            return new CarResponse(car);
        }

        return null;
    }

    public CarResponse Add(CarRequest.CreateRequest newCar)
    {
        var car = new Car(newCar);

        _context.Cars.Add(car);
        _context.SaveChanges();

        return Get(car.Id);
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

        return Get(car.Id);
    }
}