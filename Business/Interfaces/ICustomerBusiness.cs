using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICustomerBusiness
{
    IEnumerable<CustomerResponse> GetAll();
    CustomerResponse? Get(int id);
    CustomerResponse? GetCustomer(string email);
    CustomerResponse Add(CustomerRequest.CreateRequest newCustomer);
    Task<CustomerResponse> Update(CustomerRequest.UpdateRequest customer);
    Task Delete(int id);
}