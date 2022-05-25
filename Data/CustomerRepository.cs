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

    public async Task<IEnumerable<CustomerResponse>> GetAll()
    {
        return await _context.Customers
            .Select(p => new CustomerResponse(p))
            .ToListAsync();
    }

    public async Task<Customer?> Get(int id)
    {
        return await _context.Customers
            .SingleOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Customer> GetCustomerFromCarRented(int id)
    {
        return await _context.Customers
            .SingleOrDefaultAsync(p => p.CarRentedId == id);
        // var customer = await _context.Customers.SingleOrDefaultAsync(p => p.CarRentedId == id);
        // if (customer != null)
        // {
        //     return new CustomerResponse(customer);
        // }

        // return null;
    }

    public async Task<CustomerResponse?> GetCustomer(string email)
    {
        var customer = await _context.Customers
            .SingleOrDefaultAsync(p => p.Email == email);

        if (customer != null)
        {
            return new CustomerResponse(customer);
        }

        return null;
    }

    public async Task<CustomerResponse> Add(CustomerRequest.CreateRequest newCustomer)
    {
        var transaction = _context.Database.BeginTransaction();
        var customer = new Customer(newCustomer);

        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();

        return new CustomerResponse(customer);
    }

    public async Task Delete(Customer customer)
    {
        var transaction = _context.Database.BeginTransaction();
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
        await transaction.CommitAsync();
    }

    public async Task<CustomerResponse> Update(Customer customer)
    {
        try
        {
            var transaction = _context.Database.BeginTransaction();
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return new CustomerResponse(customer);
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}