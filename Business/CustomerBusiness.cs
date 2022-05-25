using CentRent.Models;
using CentRent.Data;
using CentRent.Interfaces;
using CentRent.Entities;

namespace CentRent.Business;

public class CustomerBusiness : ICustomerBusiness
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICarRepository _carRepository;

    public CustomerBusiness(ICustomerRepository customerRepository, ICarRepository carRepository)
    {
        _customerRepository = customerRepository;
        _carRepository = carRepository;
    }

    public IEnumerable<CustomerResponse> GetAll()
    {
        return _customerRepository.GetAll();
    }

    public CustomerResponse? Get(int id)
    {
        var customer = _customerRepository.Get(id);
        if (customer != null)
        {
            return new CustomerResponse(customer);
        }
        return null;
    }

    public CustomerResponse? GetCustomer(string email)
    {
        return _customerRepository.GetCustomer(email);
    }

    public CustomerResponse Add(CustomerRequest.CreateRequest newCustomer)
    {
        var customerAdded = _customerRepository.Add(newCustomer);

        if (customerAdded != null)
        {
            // SI AÑADIMOS UN CLIENTE NUEVO CON COCHE ALQUILADO
            // SETEAMOS EL COCHE COMO ALQUILADO
            if (customerAdded.CarRentedId > 0)
            {
                var carResponse = _carRepository.Get(newCustomer.CarRentedId);

                if (carResponse != null)
                {
                    var carRequest = new Car(carResponse, 1);

                    var carUpdated = _carRepository.Update(carRequest);
                }
            }
        }

        return customerAdded;
    }

    public async Task Delete(int id)
    {
        var customerToDelete = _customerRepository.Get(id);

        if (customerToDelete != null)
        {
            // SI SE ELIMINA UN CLIENTE SU COCHE ALQUILADO PASA A ESTAR DISPONIBLE
            if (customerToDelete.CarRentedId > 0)
            {
                var carResponse = _carRepository.Get(customerToDelete.CarRentedId);

                if (carResponse != null)
                {
                    var carRequest = new Car(carResponse, 0);

                    _carRepository.Update(carRequest);
                }
            }

            await _customerRepository.Delete(customerToDelete);
        }
    }

    public async Task<CustomerResponse> Update(CustomerRequest.UpdateRequest customer)
    {
        var oldCustomer = _customerRepository.Get(customer.Id);
        var carToUpdate = new Car();

        if (oldCustomer != null)
        {
            // SI MODIFICAMOS EL CLIENTE PARA ALQUILAR UN COCHE
            // SETEAMOS ESE COCHE COMO ALQUILADO (1)
            if (customer.CarRentedId > 0 && oldCustomer.CarRentedId == 0)
            {
                return await CustomerAddCar(customer, carToUpdate);
            }
            // SI EL CLIENTE DEJA DE ALQUILAR UN COCHE, SETEAMOS
            // EL COCHE COMO NO ALQUILADO (0)
            if (oldCustomer.CarRentedId > 0 && customer.CarRentedId == 0)
            {
                return await CustomerRemoveCar(customer, oldCustomer.CarRentedId, carToUpdate);
            }
            // SI EL CLIENTE CAMBIA EL COCHE DE ALQUILER, SETEAMOS EL ANTIGUO
            // A 0 Y LE ASIGNAMOS EL NUEVO COCHE
            if (oldCustomer.CarRentedId > 0 && customer.CarRentedId > 0 &&
                oldCustomer.CarRentedId != customer.CarRentedId)
            {
                return await CustomerChangeCar(customer, oldCustomer.CarRentedId, carToUpdate);
            }
        }

        return null;
    }

    public async Task<CustomerResponse> CustomerAddCar(CustomerRequest.UpdateRequest customer, Car carToUpdate)
    {
        var carResponse = _carRepository.Get(customer.CarRentedId);
        if (carResponse != null)
        {
            carToUpdate.Id = carResponse.Id;
            carToUpdate.Registration = carResponse.Registration;
            carToUpdate.IsRented = 1;
            carToUpdate.CarTypeId = carResponse.CarTypeId;
        }

        // ACTUALIZAMOS EL COCHE
        _carRepository.Update(carToUpdate);

        // ACTUALIZAMOS EL CLIENTE
        var customerToUpdate = _customerRepository.Get(customer.Id);
        if (customerToUpdate != null)
        {
            customerToUpdate.CarRentedId = customer.CarRentedId;
            return await _customerRepository.Update(customerToUpdate);
        }

        return null;
    }

    public async Task<CustomerResponse> CustomerRemoveCar(CustomerRequest.UpdateRequest customer, int carId, Car carToUpdate)
    {
        var carResponse = _carRepository.Get(carId);
        if (carResponse != null)
        {
            carToUpdate.Id = carResponse.Id;
            carToUpdate.Registration = carResponse.Registration;
            carToUpdate.IsRented = 0;
            carToUpdate.CarTypeId = carResponse.CarTypeId;
        }

        // ACTUALIZAMOS EL COCHE
        _carRepository.Update(carToUpdate);

        // ACTUALIZAMOS EL CLIENTE
        var customerToUpdate = _customerRepository.Get(customer.Id);
        if (customerToUpdate != null)
        {
            customerToUpdate.CarRentedId = 0;
            return await _customerRepository.Update(customerToUpdate);
        }

        return null;
    }

    public async Task<CustomerResponse> CustomerChangeCar(CustomerRequest.UpdateRequest customer, int carId, Car carToUpdate)
    {
        var oldCarResponse = _carRepository.Get(carId);
        if (oldCarResponse != null)
        {
            carToUpdate.Id = oldCarResponse.Id;
            carToUpdate.Registration = oldCarResponse.Registration;
            carToUpdate.IsRented = 0;
            carToUpdate.CarTypeId = oldCarResponse.CarTypeId;
        }

        var changeCarResponse = _carRepository.Get(customer.CarRentedId);
        var carUpdated = new Car();
        if (changeCarResponse != null)
        {
            carUpdated.Id = changeCarResponse.Id;
            carUpdated.Registration = changeCarResponse.Registration;
            carUpdated.IsRented = 1;
            carUpdated.CarTypeId = changeCarResponse.CarTypeId;
        }

        // ACTUALIZAMOS LOS COCHE
        _carRepository.Update(carToUpdate);
        _carRepository.Update(carUpdated);

        // ACTUALIZAMOS EL CLIENTE
        var customerToUpdate = _customerRepository.Get(customer.Id);
        if (customerToUpdate != null)
        {
            customerToUpdate.CarRentedId = customer.CarRentedId;
            return await _customerRepository.Update(customerToUpdate);
        }

        return null;
    }
}