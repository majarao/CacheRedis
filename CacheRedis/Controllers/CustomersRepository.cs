using CacheRedis.Abstractions;
using CacheRedis.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CacheRedis.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomersRepository(ICustomerRepository customerRepository) : ControllerBase
{
    private ICustomerRepository CustomerRepository { get; } = customerRepository;

    [HttpPost]
    public ActionResult<Customer> Create(Customer customer)
    {
        customer = CustomerRepository.Create(customer);
        return Ok(customer);
    }

    [HttpDelete("customerId:int")]
    public ActionResult Delete(int customerId)
    {
        Customer? customer = CustomerRepository.ReadById(customerId);
        if (customer is null)
            return NoContent();

        if (!CustomerRepository.Delete(customerId))
            return BadRequest();

        return Ok();
    }

    [HttpGet]
    public ActionResult<IQueryable<Customer>> Read()
    {
        IQueryable<Customer>? customers = CustomerRepository.Read();
        if (customers!.Any())
            return Ok(customers);

        return NoContent();
    }

    [HttpGet("customerId:int")]
    public ActionResult<Customer> ReadById(int customerId)
    {
        Customer? customer = CustomerRepository.ReadById(customerId);
        if (customer is null)
            return NoContent();

        return Ok(customer);
    }

    [HttpPut("customerId:int")]
    public ActionResult<bool> Update(int customerId, Customer customer)
    {
        if (customer is null)
            return NoContent();

        if (customerId != customer.CustomerId)
            return BadRequest();

        customer = CustomerRepository.Update(customer);
        return Ok(customer);
    }
}
