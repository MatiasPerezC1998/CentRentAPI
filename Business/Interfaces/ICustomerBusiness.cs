using CentRent.Models;

namespace CentRent.Interfaces;

public interface ICustomerBusiness
{
    IEnumerable<CustomerResponse> GetAll();
    CustomerResponse? Get(int id);
    CustomerResponse? GetCustomer(string name);
    CustomerResponse Add(CustomerRequest.CreateRequest newCustomer);
    CustomerResponse Update(CustomerRequest.UpdateRequest customer);
    void Delete(int id);
}