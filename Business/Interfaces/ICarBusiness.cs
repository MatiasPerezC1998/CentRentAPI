using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICarBusiness
{
    IEnumerable<CarResponse> GetAll();
    CarResponse? Get(int id);
    CarResponse? GetCar(string registration);
    IEnumerable<Car> GetAvailableCarsFromType();
    CarResponse Add(CarRequest.CreateRequest newCar);
    CarResponse Update(CarRequest.UpdateRequest car);
    Task Delete(int id);
    Task CustomerFinishRentingFromType(IEnumerable<Car> carsToDelete);
}