using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface ICarRepository
{
    IEnumerable<CarResponse> GetAll();
    CarResponse? Get(int id);
    CarResponse? GetCar(string registration);
    Task<List<Car>> GetCarTypeId(int carTypeId);
    IEnumerable<Car> GetAvailableCars();
    CarResponse Add(CarRequest.CreateRequest newCar);
    CarResponse Update(Car car);
    Task Delete(Car car);
    Task DeleteCarsFromType(IEnumerable<Car> carsToDelete);
}