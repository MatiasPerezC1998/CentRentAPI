using CentRent.Models;
using CentRent.Data;
using CentRent.Interfaces;
using CentRent.Entities;

namespace CentRent.Business;

public class CarBusiness : ICarBusiness
{
    public readonly ICarRepository _carRepository;
    public readonly ICustomerRepository _customerRepository;
    public CarBusiness(ICarRepository carRepository, ICustomerRepository customerRepository)
    {
        _carRepository = carRepository;
        _customerRepository = customerRepository;
    }

    public IEnumerable<CarResponse> GetAll()
    {
        return _carRepository.GetAll();
    }

    public CarResponse? Get(int id)
    {
        return _carRepository.Get(id);
    }

    public CarResponse? GetCar(string registration)
    {
        return _carRepository.GetCar(registration);
    }

    public CarResponse Add(CarRequest.CreateRequest newCar)
    {
        return _carRepository.Add(newCar);
    }

    public void Delete(int id)
    {
        var carToDelete = _carRepository.Get(id);

        if (carToDelete != null)
        {
            // SI SE ELIMINA UN COCHE SU CLIENTE PASA A NO TENER COCHE DE ALQUILER
            if (carToDelete.IsRented > 0)
            {
                var customerResponse = _customerRepository.GetCustomerFromCarRented(carToDelete.Id);

                if (customerResponse != null)
                {
                    var customerRequest = new Customer(customerResponse, 0);

                    _customerRepository.Update(customerRequest);
                }
            }

            var car = new Car(carToDelete);

            _carRepository.Delete(car);
        }
    }

    public CarResponse Update(CarRequest.UpdateRequest car)
    {
        var getCar = _carRepository.Get(car.Id);

        if (getCar != null)
        {
            var carToUpdate = new Car()
            {
                Id = getCar.Id,
                Registration = car.Registration ?? getCar.Registration,
                CarTypeId = car.CarTypeId
            };

            return _carRepository.Update(carToUpdate);
        }

        return null;
    }
}