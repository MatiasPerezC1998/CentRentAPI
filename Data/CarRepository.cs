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

    public async Task<List<Car>> GetAll()
    {
        return await _context.Cars
            .Include(x => x.CarType)
            .Select(p => p)
            .ToListAsync();
    }

    public async Task<Car?> Get(int id)
    {
        var car = await _context.Cars
            .Include(x => x.CarType)
            .SingleOrDefaultAsync(p => p.Id == id);

        return car;
    }

    public async Task<Car?> GetCar(string registration)
    {
        var car = await _context.Cars
            .Include(x => x.CarType)
            .SingleOrDefaultAsync(p => p.Registration == registration);

        if (car != null)
        {
            return car;
        }

        return null;
    }

    public async Task<IEnumerable<Car>> GetAvailableCars()
    {
        var cars = await _context.Cars
            .Include(x => x.CarType)
            .Where(car => car.IsRented == 0)
            .ToListAsync();
        return cars;
    }

    public async Task<Car> Add(Car newCar)
    {
        var transaction = _context.Database.BeginTransaction();

        _context.Cars.Add(newCar);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return await Get(newCar.Id);
    }

    public async Task Delete(Car car)
    {
        try
        {
            var transaction = _context.Database.BeginTransaction();
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }

    }


    public async Task<Car> Update(Car car)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.Cars.Update(car);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return await Get(car.Id);
    }

    public async Task<List<Car>> GetCarTypeId(int carTypeId)
    {
        var carList = await _context.Cars
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