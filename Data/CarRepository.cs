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

    public async Task<IEnumerable<CarResponse>> GetAll()
    {
        return await _context.Cars
            .Include(x => x.CarType)
            .Select(p => new CarResponse(p))
            .ToListAsync();
    }

    public async Task<CarResponse?> Get(int id)
    {
        var car = await _context.Cars
            .Include(x => x.CarType)
            .SingleOrDefaultAsync(p => p.Id == id);

        return new CarResponse(car);
    }

    public async Task<CarResponse?> GetCar(string registration)
    {
        var car = await _context.Cars
            .Include(x => x.CarType)
            .SingleOrDefaultAsync(p => p.Registration == registration);

        if (car != null)
        {
            return new CarResponse(car);
        }

        return null;
    }

    public async Task<IEnumerable<Car>> GetAvailableCars()
    {
        var cars = await _context.Cars
            .Where(car => car.IsRented == 0)
            .ToListAsync();
        return cars;
    }

    public async Task<CarResponse> Add(CarRequest.CreateRequest newCar)
    {
        var transaction = _context.Database.BeginTransaction();
        var car = new Car(newCar);

        _context.Cars.Add(car);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return await Get(car.Id);
    }

    public async Task Delete(Car car)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.Cars.Remove(car);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }


    public async Task<CarResponse> Update(Car car)
    {
        var carToUpdate = await _context.Cars.SingleOrDefaultAsync(x => x.Id == car.Id);
        if (carToUpdate != null)
        {
            carToUpdate.CarTypeId = car.CarTypeId;
            carToUpdate.IsRented = car.IsRented;
            carToUpdate.Registration = car.Registration;

            var transaction = _context.Database.BeginTransaction();
            _context.Cars.Update(carToUpdate);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }

        return await Get(car.Id);
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