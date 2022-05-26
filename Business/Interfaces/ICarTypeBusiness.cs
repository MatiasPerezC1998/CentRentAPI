using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICarTypeBusiness
{
    Task<IEnumerable<CarTypeResponse>> GetAll();
    Task<CarTypeResponse?> Get(int id);
    Task<CarTypeResponse> Add(CarTypeRequest.CreateRequest newCar);
    Task<CarTypeResponse> Update(CarTypeRequest.UpdateRequest car);
    Task Delete(int id);
}