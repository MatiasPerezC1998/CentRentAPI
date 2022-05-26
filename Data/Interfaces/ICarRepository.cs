using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface ICarRepository
{
    Task<List<Car>> GetAll();
    Task<Car?> Get(int id);
    Task<Car?> GetCar(string registration);
    Task<List<Car>> GetCarTypeId(int carTypeId);
    Task<IEnumerable<Car>> GetAvailableCars();
    Task<Car> Add(Car newCar);
    Task<Car> Update(Car car);
    Task Delete(Car car);
    Task DeleteCarsFromType(IEnumerable<Car> carsToDelete);
}