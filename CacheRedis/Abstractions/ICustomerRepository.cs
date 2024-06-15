using CacheRedis.Entities;

namespace CacheRedis.Abstractions;

public interface ICustomerRepository
{
    Customer Create(Customer entity);
    Customer Update(Customer entity);
    bool Delete(int customerId);
    IQueryable<Customer>? Read();
    Customer? ReadById(int customerId);
}
