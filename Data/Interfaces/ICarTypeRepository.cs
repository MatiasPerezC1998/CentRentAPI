using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface ICarTypeRepository
{
    IEnumerable<CarTypeResponse> GetAll();
    CarType? Get(int id);
    CarTypeResponse Add(CarTypeRequest.CreateRequest newCar);
    CarTypeResponse Update(CarType car);
    Task Delete(CarType car);
}