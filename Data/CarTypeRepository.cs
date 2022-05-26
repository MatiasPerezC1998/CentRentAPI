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

    public async Task<IEnumerable<CarType>> GetAll()
    {
        return await _context.CarTypes
            .Include(x => x.Cars)
            .Select(p => p)
            .ToListAsync();
    }

    public async Task<CarType?> Get(int id)
    {
        return await _context.CarTypes
            .Include(x => x.Cars)
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<CarType> Add(CarType newCarType)
    {
        var transaction = _context.Database.BeginTransaction();

        _context.CarTypes.Add(newCarType);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return newCarType;
    }

    public async Task Delete(CarType carType)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.CarTypes.Remove(carType);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }


    public async Task<CarType> Update(CarType carType)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.CarTypes.Update(carType);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return carType;
    }

}