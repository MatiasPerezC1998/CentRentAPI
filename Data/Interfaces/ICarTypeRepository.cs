using CentRent.Entities;
using CentRent.Models;

namespace CentRent.Data;

public interface ICarTypeRepository
{
    IEnumerable<CarTypeResponse> GetAll();
    Task<CarType?> Get(int id);
    Task<CarTypeResponse> Add(CarTypeRequest.CreateRequest newCar);
    Task<CarTypeResponse> Update(CarType car);
    Task Delete(CarType car);
}