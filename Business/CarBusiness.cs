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

    public async Task<IEnumerable<CarResponse>> GetAll()
    {
        var allCars = await _carRepository.GetAll();
        return new List<CarResponse> (allCars.Select(p => new CarResponse(p)));
    }

    public async Task<CarResponse?> Get(int id)
    {
        return new CarResponse (await _carRepository.Get(id));
    }

    public async Task<CarResponse?> GetCar(string registration)
    {
        return new CarResponse(await _carRepository.GetCar(registration));
    }

    public async Task<IEnumerable<CarResponse>> GetAvailableCarsFromType()
    {
        var allAvailableCars = await _carRepository.GetAvailableCars();
        return new List<CarResponse> (allAvailableCars.Select(x => new CarResponse(x)));
    }

    public async Task<CarResponse> Add(CarRequest.CreateRequest newCar)
    {
        var carToAdd = new Car(newCar);
        return new CarResponse (await _carRepository.Add(carToAdd));
    }

    public async Task Delete(CarResponse carToDelete)
    {
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

            var car = await _carRepository.Get(carToDelete.Id);

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

    public async Task<CarResponse> Update(CarRequest.UpdateRequest car)
    {
        var getCar = await _carRepository.Get(car.Id);

        if (getCar != null)
        {
            var carToUpdate = new Car()
            {
                Id = getCar.Id,
                Registration = car.Registration ?? getCar.Registration,
                CarTypeId = car.CarTypeId
            };
            


            return new CarResponse(await _carRepository.Update(carToUpdate));
        }

        return null;
    }
}