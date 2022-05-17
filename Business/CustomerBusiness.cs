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
        return _customerRepository.Get(id);
    }

    public CustomerResponse? GetCustomer(string name)
    {
        return _customerRepository.GetCustomer(name);
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
                    var carRequest = new Car
                    {
                        Id = carResponse.Id,
                        Name = carResponse.Name,
                        Brand = carResponse.Brand,
                        Type = carResponse.Type,
                        IsRented = 1,
                        Registration = carResponse.Registration,
                    };
                    var carUpdated = _carRepository.Update(carRequest);
                }
            }
        }

        return customerAdded;
    }

    public void Delete(int id)
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
                    var carRequest = new Car
                    {
                        Id = carResponse.Id,
                        Name = carResponse.Name,
                        Brand = carResponse.Brand,
                        Type = carResponse.Type,
                        IsRented = 0,
                        Registration = carResponse.Registration,
                    };

                    _carRepository.Update(carRequest);
                }
            }

            var customer = new Customer()
            {
                Id = customerToDelete.Id,
                Name = customerToDelete.Name,
                Surname = customerToDelete.Surname, 
                Email = customerToDelete.Email,
                Phone = customerToDelete.Phone,
                Dni = customerToDelete.Dni,
                CarRentedId = customerToDelete.CarRentedId
            };

            _customerRepository.Delete(customer);
        }
    }

    public CustomerResponse Update(CustomerRequest.UpdateRequest customer)
    {
        var oldCustomer = _customerRepository.Get(customer.Id);
        var carToUpdate = new Car();

        if (oldCustomer != null)
        {
            // SI MODIFICAMOS EL CLIENTE PARA ALQUILAR UN COCHE
            // SETEAMOS ESE COCHE COMO ALQUILADO (1)
            if (customer.CarRentedId > 0 && oldCustomer.CarRentedId == 0)
            {
                return CustomerAddCar(customer, oldCustomer, carToUpdate);
            }
            // SI EL CLIENTE DEJA DE ALQUILAR UN COCHE, SETEAMOS
            // EL COCHE COMO NO ALQUILADO (0)
            if (oldCustomer.CarRentedId > 0 && customer.CarRentedId == 0)
            {
                return CustomerRemoveCar(customer, oldCustomer, carToUpdate);
            }
            // SI EL CLIENTE CAMBIA EL COCHE DE ALQUILER, SETEAMOS EL ANTIGUO
            // A 0 Y LE ASIGNAMOS EL NUEVO COCHE
            if (oldCustomer.CarRentedId > 0 && customer.CarRentedId > 0 &&
                oldCustomer.CarRentedId != customer.CarRentedId)
            {
                return CustomerChangeCar(customer, oldCustomer, carToUpdate);
            }
        }

        return null;
    }

    public CustomerResponse CustomerAddCar(CustomerRequest.UpdateRequest customer, CustomerResponse oldCustomer, Car carToUpdate)
    {
        var carResponse = _carRepository.Get(customer.CarRentedId);
        if (carResponse != null)
        {
            carToUpdate.Id = carResponse.Id;
            carToUpdate.Name = carResponse.Name;
            carToUpdate.Brand = carResponse.Brand;
            carToUpdate.Type = carResponse.Type;
            carToUpdate.Registration = carResponse.Registration;
            carToUpdate.IsRented = 1;
        }

        // ACTUALIZAMOS EL COCHE
        _carRepository.Update(carToUpdate);

        // ACTUALIZAMOS EL CLIENTE
        var customerToUpdate = new Customer()
        {
            Id = customer.Id,
            Name = customer.Name,
            Surname = customer.Surname,
            Email = customer.Email,
            Phone = customer.Phone,
            Dni = customer.Dni,
            CarRentedId = customer.CarRentedId
        };
        
        return _customerRepository.Update(customerToUpdate);
    }

    public CustomerResponse CustomerRemoveCar(CustomerRequest.UpdateRequest customer, CustomerResponse oldCustomer, Car carToUpdate)
    {
        var carResponse = _carRepository.Get(oldCustomer.CarRentedId);
        if (carResponse != null)
        {
            carToUpdate.Id = carResponse.Id;
            carToUpdate.Name = carResponse.Name;
            carToUpdate.Brand = carResponse.Brand;
            carToUpdate.Type = carResponse.Type;
            carToUpdate.Registration = carResponse.Registration;
            carToUpdate.IsRented = 0;
        }

        // ACTUALIZAMOS EL COCHE
        _carRepository.Update(carToUpdate);

        // ACTUALIZAMOS EL CLIENTE
        var customerToUpdate = new Customer()
        {
            Id = customer.Id,
            Name = customer.Name,
            Surname = customer.Surname,
            Email = customer.Email,
            Phone = customer.Phone,
            Dni = customer.Dni,
            CarRentedId = customer.CarRentedId
        };

        return _customerRepository.Update(customerToUpdate);
    }

    public CustomerResponse CustomerChangeCar(CustomerRequest.UpdateRequest customer, CustomerResponse oldCustomer, Car carToUpdate)
    {
        var oldCarResponse = _carRepository.Get(oldCustomer.CarRentedId);
        if (oldCarResponse != null)
        {
            carToUpdate.Id = oldCarResponse.Id;
            carToUpdate.Name = oldCarResponse.Name;
            carToUpdate.Brand = oldCarResponse.Brand;
            carToUpdate.Type = oldCarResponse.Type;
            carToUpdate.Registration = oldCarResponse.Registration;
            carToUpdate.IsRented = 0;
        }

        var changeCarResponse = _carRepository.Get(customer.CarRentedId);
        var carUpdated = new Car();
        if (changeCarResponse != null)
        {
            carUpdated.Id = changeCarResponse.Id;
            carUpdated.Name = changeCarResponse.Name;
            carUpdated.Brand = changeCarResponse.Brand;
            carUpdated.Type = changeCarResponse.Type;
            carUpdated.Registration = changeCarResponse.Registration;
            carUpdated.IsRented = 1;
        }

        // ACTUALIZAMOS LOS COCHE
        _carRepository.Update(carToUpdate);
        _carRepository.Update(carUpdated);

        // ACTUALIZAMOS EL CLIENTE
        var newCarResponse = _carRepository.Get(customer.CarRentedId);

        var customerToUpdate = new Customer()
        {
            Id = customer.Id,
            Name = customer.Name,
            Surname = customer.Surname,
            Email = customer.Email,
            Phone = customer.Phone,
            Dni = customer.Dni,
            CarRentedId = (newCarResponse != null) ? newCarResponse.Id : 0
        };

        return _customerRepository.Update(customerToUpdate);
    }
}