using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Data;

public interface ICustomerRepository
{
    Task<IEnumerable<CustomerResponse>> GetAll();
    Task<Customer?> Get(int id);
    Task<Customer> GetCustomerFromCarRented(int id);
    Task<CustomerResponse?> GetCustomer(string email);
    Task<CustomerResponse> Add(CustomerRequest.CreateRequest newCustomer);
    Task<CustomerResponse> Update(Customer customer);
    Task Delete(Customer customer);
}