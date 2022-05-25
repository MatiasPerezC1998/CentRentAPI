using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICustomerBusiness
{
    Task<IEnumerable<CustomerResponse>> GetAll();
    Task<CustomerResponse?> Get(int id);
    Task<CustomerResponse?> GetCustomer(string email);
    Task<CustomerResponse> Add(CustomerRequest.CreateRequest newCustomer);
    Task<CustomerResponse> Update(CustomerRequest.UpdateRequest customer);
    Task Delete(int id);
}