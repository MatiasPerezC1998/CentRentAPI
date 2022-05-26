using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface ICarTypeRepository
{
    Task<IEnumerable<CarType>> GetAll();
    Task<CarType?> Get(int id);
    Task<CarType> Add(CarType newCar);
    Task<CarType> Update(CarType car);
    Task Delete(CarType car);
}