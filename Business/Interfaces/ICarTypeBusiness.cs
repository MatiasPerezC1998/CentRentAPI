using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICarTypeBusiness
{
    IEnumerable<CarTypeResponse> GetAll();
    CarTypeResponse? Get(int id);
    CarTypeResponse Add(CarTypeRequest.CreateRequest newCar);
    CarTypeResponse Update(CarTypeRequest.UpdateRequest car);
    Task Delete(int id);
}