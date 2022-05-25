using Microsoft.EntityFrameworkCore;
using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Data;

public class CarTypeRepository : ICarTypeRepository
{
    private readonly CentRentContext _context;
    public CarTypeRepository(CentRentContext context)
    {
        _context = context;
    }

    public IEnumerable<CarTypeResponse> GetAll()
    {
        return _context.CarTypes
            .Include(x => x.Cars)
            .Select(p => new CarTypeResponse(p))
            .ToList();
    }

    public CarType? Get(int id)
    {
        return _context.CarTypes
            .Include(x => x.Cars)
            .SingleOrDefault(p => p.Id == id);
    }

    public CarTypeResponse Add(CarTypeRequest.CreateRequest newCarType)
    {
        var transaction = _context.Database.BeginTransaction();
        var carType = new CarType(newCarType);

        _context.CarTypes.Add(carType);
        _context.SaveChanges();
        transaction.Commit();

        return new CarTypeResponse(carType);
    }

    public async Task Delete(CarType carType)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.CarTypes.Remove(carType);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }


    public CarTypeResponse Update(CarType carType)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.CarTypes.Update(carType);
        _context.SaveChanges();
        transaction.Commit();

        return new CarTypeResponse(carType);
    }

}