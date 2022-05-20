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
            .AsNoTracking()
            .ToList();
    }

    public CarTypeResponse? Get(int id)
    {
        var carType = _context.CarTypes
            .Include(x => x.Cars)
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);

        return new CarTypeResponse(carType);
    }

    public CarTypeResponse Add(CarTypeRequest.CreateRequest newCarType)
    {
        var carType = new CarType(newCarType);

        _context.CarTypes.Add(carType);
        _context.SaveChanges();

        return new CarTypeResponse(carType);
    }

    public void Delete(CarType carType)
    {
        _context.CarTypes.Remove(carType);
        _context.SaveChanges();
    }


    public CarTypeResponse Update(CarType carType)
    {
        _context.CarTypes.Update(carType);
        _context.SaveChanges();

        return new CarTypeResponse(carType);
    }
}