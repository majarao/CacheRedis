using CacheRedis.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CacheRedis.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(c => c.CustomerId);
        builder.Property(c => c.CustomerName).IsRequired().HasMaxLength(50);
        builder.Property(c => c.CustomerEmail).IsRequired().HasMaxLength(50);

        List<Customer> customers = [];
        for (int i = 1; i <= 100; i++)
            customers.Add(new() { CustomerId = i, CustomerName = $"Customer {i}", CustomerEmail = $"customer_{i}@company.com" });

        builder.HasData(customers);
    }
}
