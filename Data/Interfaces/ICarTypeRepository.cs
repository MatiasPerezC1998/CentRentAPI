using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface ICarTypeRepository
{
    IEnumerable<CarTypeResponse> GetAll();
    CarTypeResponse? Get(int id);
    CarTypeResponse Add(CarTypeRequest.CreateRequest newCar);
    CarTypeResponse Update(CarType car);
    void Delete(CarType car);
}