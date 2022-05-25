using Microsoft.AspNetCore.Mvc;
using CentRent.Models;
using CentRent.Interfaces;

namespace CentRent.Controllers;

[ApiController]
[Route("[controller]")]
public class CustomerController : ControllerBase
{
    public readonly ICustomerBusiness _customerBusiness;

    public CustomerController(ICustomerBusiness customerBusiness)
    {
        _customerBusiness = customerBusiness;
    }

    [HttpGet("GetAll")]
    public async Task<IEnumerable<CustomerResponse>> GetAll()
    {
        return await _customerBusiness.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CustomerResponse>> Get(int id)
    {
        var customer = await _customerBusiness.Get(id);

        if (customer is not null)
        {
            return customer;
            
        } else {

            return NotFound();

        }
    }

    [HttpGet("Email/{email}")]
    public async Task<ActionResult<CustomerResponse>> GetCustomer(string email)
    {
        var customer = await _customerBusiness.GetCustomer(email);

        if (customer is not null)
        {
            return customer;
        } else {
            return NotFound("No existe ningún cliente con ese nombre");
        }
    }

    [HttpPost("Create")]
    public async Task<ActionResult<CustomerResponse>> Create([FromForm] CustomerRequest.CreateRequest customer)
    {
        var newCustomer = await _customerBusiness.Add(customer);
        return Ok(newCustomer);
    }

    [HttpPost("Update")]
    public async Task<ActionResult<CustomerResponse>> Update([FromForm] CustomerRequest.UpdateRequest customer)
    {
        var customerUpdated = await _customerBusiness.Update(customer);

        if (customerUpdated != null)
        {
            return Ok(customerUpdated);
        }

        return BadRequest("No existe el cliente con el id " + customer.Id);
    }

    [HttpPost("Delete")]
    public async Task<IActionResult> Delete([FromForm] int id)
    {
        var customer = await _customerBusiness.Get(id);

        if (customer is null)
            return NotFound();

        await _customerBusiness.Delete(id);

        return Ok();
    }
}