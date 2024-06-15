using System.ComponentModel.DataAnnotations;

namespace CacheRedis.Entities;

public class Customer
{
    public int CustomerId { get; set; }
    public string CustomerName { get; set; } = string.Empty;

    [EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;
}
