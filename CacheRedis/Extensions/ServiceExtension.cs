using CacheRedis.Abstractions;
using CacheRedis.Context;
using CacheRedis.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CacheRedis.Extensions;

public static class ServiceExtension
{
    public static IServiceCollection AddDbContext(this IServiceCollection services, ConfigurationManager configurationManager)
    {
        services.AddDbContext<AppDbContext>(option => option
            .UseSqlServer(configurationManager.GetConnectionString("DefaultConnection")));

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configurationManager.GetValue<string>("CacheSettings:ConnectionString");
        });

        services.AddScoped<ICustomerRepository, CustomerRepository>();

        return services;
    }
}
