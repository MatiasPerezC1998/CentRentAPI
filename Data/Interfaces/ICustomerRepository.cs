using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Data;

public interface ICustomerRepository
{
    Task<IEnumerable<Customer>> GetAll();
    Task<Customer?> Get(int id);
    Task<Customer> GetCustomerFromCarRented(int id);
    Task<Customer?> GetCustomer(string email);
    Task<Customer> Add(Customer newCustomer);
    Task<Customer> Update(Customer customer);
    Task Delete(Customer customer);
}