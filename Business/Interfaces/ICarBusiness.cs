using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICarBusiness
{
    Task<IEnumerable<CarResponse>> GetAll();
    Task<CarResponse?> Get(int id);
    Task<CarResponse?> GetCar(string registration);
    Task<IEnumerable<CarResponse>> GetAvailableCarsFromType();
    Task<CarResponse> Add(CarRequest.CreateRequest newCar);
    Task<CarResponse> Update(CarRequest.UpdateRequest car);
    Task Delete(CarResponse carToDelete);
    Task CustomerFinishRentingFromType(IEnumerable<Car> carsToDelete);
}