using CentRent.Models;
using CentRent.Entities;

namespace CentRent.Data;

public interface ICustomerRepository
{
    IEnumerable<CustomerResponse> GetAll();
    CustomerResponse? Get(int id);
    CustomerResponse GetCustomerFromCarRented(int id);
    CustomerResponse? GetCustomer(string email);
    CustomerResponse Add(CustomerRequest.CreateRequest newCustomer);
    CustomerResponse Update(Customer customer);
    void Delete(Customer customer);
}