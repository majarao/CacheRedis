using CacheRedis.Abstractions;
using CacheRedis.Context;
using CacheRedis.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace CacheRedis.Repositories;

public class CustomerRepository(AppDbContext appDbContext, IDistributedCache redisCache) : ICustomerRepository
{
    private AppDbContext AppDbContext { get; } = appDbContext;
    private IDistributedCache RedisCache { get; } = redisCache;

    public Customer Create(Customer customer)
    {
        AppDbContext.Set<Customer>().Add(customer);
        AppDbContext.SaveChanges();
        return customer;
    }

    public bool Delete(int customerId)
    {
        Customer? customer = ReadById(customerId);
        if (customer is not null)
        {
            AppDbContext.Set<Customer>().Remove(customer);
            AppDbContext.SaveChanges();
            return true;
        }

        return false;
    }

    public IQueryable<Customer>? Read() => AppDbContext.Set<Customer>();
    public Customer? ReadById(int customerId)
    {
        string? customerCache = RedisCache.GetString(customerId.ToString());
        Customer? customer;

        if (string.IsNullOrEmpty(customerCache))
        {
            customer = Read()?.FirstOrDefault(c => c.CustomerId == customerId);
            if (customer is not null)
                RedisCache.SetString(customerId.ToString(), JsonSerializer.Serialize<Customer>(customer));
        }
        else
        {
            customer = JsonSerializer.Deserialize<Customer>(customerCache);
            if (customer is not null)
                customer.CustomerName += " from cache";
        }

        return customer;
    }

    public Customer Update(Customer customer)
    {
        AppDbContext.Set<Customer>().Update(customer);
        return customer;
    }
}
