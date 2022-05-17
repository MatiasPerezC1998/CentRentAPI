using Microsoft.EntityFrameworkCore;
using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Data;

public class CustomerRepository : ICustomerRepository
{
    private readonly CentRentContext _context;

    public CustomerRepository(CentRentContext context)
    {
        _context = context;
    }

    public IEnumerable<CustomerResponse> GetAll()
    {
        return _context.Customers
            .Select(p => new CustomerResponse(p))
            .AsNoTracking()
            .ToList();
    }

    public CustomerResponse? Get(int id)
    {
        var customer = _context.Customers
            .AsNoTracking()
            .SingleOrDefault(p => p.Id == id);

        return new CustomerResponse(customer);
    }

    public CustomerResponse GetCustomerFromCarRented(int id)
    {
        var customer = _context.Customers
            .AsNoTracking()
            .SingleOrDefault(p => p.CarRentedId == id);

        return new CustomerResponse(customer);
    }

    public CustomerResponse? GetCustomer(string name)
    {
        var customer = _context.Customers
            .AsNoTracking()
            .SingleOrDefault(p => p.Name == name);
        if (customer != null)
        {
            return new CustomerResponse(customer);
        }

        return null;
    }

    public CustomerResponse Add(CustomerRequest.CreateRequest newCustomer)
    {
        var customer = new Customer
        {
            Name = newCustomer.Name,
            Surname = newCustomer.Surname,
            Email = newCustomer.Email,
            Phone = newCustomer.Phone,
            Dni = newCustomer.Dni,
            CarRentedId = newCustomer.CarRentedId
        };

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return new CustomerResponse(customer);
    }

    public void Delete(Customer customer)
    {
        _context.Customers.Remove(customer);
        _context.SaveChanges();
    }

    public CustomerResponse Update(Customer customer)
    {
        _context.Customers.Update(customer);
        _context.SaveChanges();

        return new CustomerResponse(customer);
    }
}