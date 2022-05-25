using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface ICarRepository
{
    Task<IEnumerable<CarResponse>> GetAll();
    Task<CarResponse?> Get(int id);
    Task<CarResponse?> GetCar(string registration);
    Task<List<Car>> GetCarTypeId(int carTypeId);
    Task<IEnumerable<Car>> GetAvailableCars();
    Task<CarResponse> Add(CarRequest.CreateRequest newCar);
    Task<CarResponse> Update(Car car);
    Task Delete(Car car);
    Task DeleteCarsFromType(IEnumerable<Car> carsToDelete);
}