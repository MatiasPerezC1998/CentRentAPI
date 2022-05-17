using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICarBusiness
{
    IEnumerable<CarResponse> GetAll();
    CarResponse? Get(int id);
    CarResponse Add(CarRequest.CreateRequest newCar);
    CarResponse Update(CarRequest.UpdateRequest car);
    void Delete(int id);
}