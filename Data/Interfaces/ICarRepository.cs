using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface ICarRepository
{
    IEnumerable<CarResponse> GetAll();
    CarResponse? Get(int id);
    CarResponse Add(CarRequest.CreateRequest newCar);
    CarResponse Update(Car car);
    void Delete(Car car);
}