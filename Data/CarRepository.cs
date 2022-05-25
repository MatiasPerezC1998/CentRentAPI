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
            .ToList();
    }

    public CarResponse? Get(int id)
    {
        var car = _context.Cars
            .Include(x => x.CarType)
            .SingleOrDefault(p => p.Id == id);

        return new CarResponse(car);
    }

    public CarResponse? GetCar(string registration)
    {
        var car = _context.Cars
            .Include(x => x.CarType)
            .SingleOrDefault(p => p.Registration == registration);

        if (car != null)
        {
            return new CarResponse(car);
        }

        return null;
    }

    public IEnumerable<Car> GetAvailableCars()
    {
        var cars = _context.Cars
            .Where(car => car.IsRented == 0)
            .ToList();
        return cars;
    }

    public CarResponse Add(CarRequest.CreateRequest newCar)
    {
        var transaction = _context.Database.BeginTransaction();
        var car = new Car(newCar);

        _context.Cars.Add(car);
        _context.SaveChanges();
        transaction.Commit();

        return Get(car.Id);
    }

    public async Task Delete(Car car)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }


    public CarResponse Update(Car car)
    {
        var carToUpdate = _context.Cars.SingleOrDefault(x => x.Id == car.Id);
        if (carToUpdate != null)
        {
            carToUpdate.CarTypeId = car.CarTypeId;
            carToUpdate.IsRented = car.IsRented;
            carToUpdate.Registration = car.Registration;

            var transaction = _context.Database.BeginTransaction();
            _context.Cars.Update(carToUpdate);
            _context.SaveChanges();
            transaction.Commit();
        }

        return Get(car.Id);
    }

    public Task<List<Car>> GetCarTypeId(int carTypeId)
    {
        var carList = _context.Cars
            .Include(x => x.CarType)
            .Where(p => p.CarTypeId == carTypeId)
            .ToListAsync();

        if (carList != null)
        {
            return carList;
        }

        return null;
    }

    public async Task DeleteCarsFromType(IEnumerable<Car> carsToDelete)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.Cars.RemoveRange(carsToDelete);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }
}