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

    public async Task<IEnumerable<CustomerResponse>> GetAll()
    {
        var allCustomers = await _customerRepository.GetAll();
        return new List<CustomerResponse>(allCustomers.Select(p => new CustomerResponse(p)));
    }

    public async Task<CustomerResponse?> Get(int id)
    {
        var customer = await _customerRepository.Get(id);
        if (customer != null)
        {
            return new CustomerResponse(customer);
        }
        return null;
    }

    public async Task<CustomerResponse?> GetCustomer(string email)
    {
        return new CustomerResponse(await _customerRepository.GetCustomer(email));
    }

    public async Task<CustomerResponse> Add(CustomerRequest.CreateRequest newCustomer)
    {
        var customerToAdd = new Customer(newCustomer);
        await _customerRepository.Add(customerToAdd);

        if (customerToAdd != null)
        {
            // SI AÑADIMOS UN CLIENTE NUEVO CON COCHE ALQUILADO
            // SETEAMOS EL COCHE COMO ALQUILADO
            if (customerToAdd.CarRentedId > 0)
            {
                var car = await _carRepository.Get(newCustomer.CarRentedId);

                if (car != null)
                {
                    car.IsRented = 1;

                    var carUpdated = await _carRepository.Update(car);
                }
            }
        }

        return new CustomerResponse(customerToAdd);
    }

    public async Task Delete(int id)
    {
        var customerToDelete = await _customerRepository.Get(id);

        if (customerToDelete != null)
        {
            // SI SE ELIMINA UN CLIENTE SU COCHE ALQUILADO PASA A ESTAR DISPONIBLE
            if (customerToDelete.CarRentedId > 0)
            {
                var car = await _carRepository.Get(customerToDelete.CarRentedId);

                if (car != null)
                {
                    car.IsRented = 0;

                    await _carRepository.Update(car);
                }
            }

            await _customerRepository.Delete(customerToDelete);
        }
    }

    public async Task<CustomerResponse> Update(CustomerRequest.UpdateRequest customer)
    {
        var oldCustomer = await _customerRepository.Get(customer.Id);

        if (oldCustomer != null)
        {
            // SETEAMOS ESE COCHE COMO ALQUILADO (1)
            if (customer.CarRentedId > 0 && oldCustomer.CarRentedId == 0)
            {
                await SetCarIsrented(customer.CarRentedId, 1);
            }
            // SETEAMOS EL COCHE COMO NO ALQUILADO (0)
            if (oldCustomer.CarRentedId > 0 && customer.CarRentedId == 0)
            {
                await SetCarIsrented(oldCustomer.CarRentedId, 0);
            }
            // SETEAMOS EL ANTIGUO A 0 Y EL NUEVO A 1   
            if (oldCustomer.CarRentedId > 0 && customer.CarRentedId > 0 &&
                oldCustomer.CarRentedId != customer.CarRentedId)
            {
                await SetCarIsrented(customer.CarRentedId, 1);
                await SetCarIsrented(oldCustomer.CarRentedId, 0);
            }

            // ACTUALIZAMOS EL CLIENTE
            oldCustomer.Dni = customer.Dni;
            oldCustomer.Email = customer.Email;
            oldCustomer.Name = customer.Name;
            oldCustomer.Phone = customer.Phone;
            oldCustomer.Surname = customer.Surname;
            oldCustomer.CarRentedId = customer.CarRentedId;

            return new CustomerResponse(await _customerRepository.Update(oldCustomer));

        }

        return null;
    }

    public async Task SetCarIsrented(int carId, int isRented)
    {
        var carToUpdate = await _carRepository.Get(carId);
        if (carToUpdate != null)
        {
            carToUpdate.IsRented = isRented;

            // ACTUALIZAMOS EL COCHE
            await _carRepository.Update(carToUpdate);
        }
    }

}