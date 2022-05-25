using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Data;

public interface ICustomerRepository
{
    IEnumerable<CustomerResponse> GetAll();
    Customer? Get(int id);
    Task<Customer> GetCustomerFromCarRented(int id);
    CustomerResponse? GetCustomer(string email);
    CustomerResponse Add(CustomerRequest.CreateRequest newCustomer);
    Task<CustomerResponse> Update(Customer customer);
    Task Delete(Customer customer);
}