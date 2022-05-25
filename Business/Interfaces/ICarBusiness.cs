using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICarBusiness
{
    Task<IEnumerable<CarResponse>> GetAll();
    Task<CarResponse?> Get(int id);
    Task<CarResponse?> GetCar(string registration);
    Task<IEnumerable<Car>> GetAvailableCarsFromType();
    Task<CarResponse> Add(CarRequest.CreateRequest newCar);
    Task<CarResponse> Update(CarRequest.UpdateRequest car);
    Task Delete(int id);
    Task CustomerFinishRentingFromType(IEnumerable<Car> carsToDelete);
}