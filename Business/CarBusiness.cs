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

    public IEnumerable<Car> GetAvailableCarsFromType()
    {
        return _carRepository.GetAvailableCars();
    }

    public CarResponse Add(CarRequest.CreateRequest newCar)
    {
        return _carRepository.Add(newCar);
    }

    public async Task Delete(int id)
    {
        var carToDelete = _carRepository.Get(id);

        if (carToDelete != null)
        {
            // SI SE ELIMINA UN COCHE SU CLIENTE PASA A NO TENER COCHE DE ALQUILER
            if (carToDelete.IsRented > 0)
            {
                var customerResponse = await _customerRepository.GetCustomerFromCarRented(carToDelete.Id);

                if (customerResponse != null)
                {
                    // var customerRequest = new Customer(customerResponse, 0);
                    customerResponse.CarRentedId = 0;

                    await _customerRepository.Update(customerResponse);
                }
            }

            var car = new Car(carToDelete);

            await _carRepository.Delete(car);
        }
    }

    public async Task CustomerFinishRentingFromType(IEnumerable<Car> carsToDelete)
    {
        for (int i = 0; i < carsToDelete.Count(); i++)
        {
            if (carsToDelete.ElementAt(i).IsRented > 0)
            {
                var customerResponse = await _customerRepository.GetCustomerFromCarRented(carsToDelete.ElementAt(i).Id);

                if (customerResponse != null)
                {
                    // var customerRequest = new Customer(customerResponse, 0);
                    customerResponse.CarRentedId = 0;

                    await _customerRepository.Update(customerResponse);
                }
            }
        }

        await _carRepository.DeleteCarsFromType(carsToDelete);
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