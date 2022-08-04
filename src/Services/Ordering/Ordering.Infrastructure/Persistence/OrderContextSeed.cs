using Microsoft.Extensions.Logging;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seeded database associated with context {DbContextName}", nameof(OrderContext));
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new Order()
            {
                UserName = "pt", FirstName = "Paul", LastName = "Tierney", EmailAddress = "ptierney07@qub.ac.uk",
                AddressLine = "Belfast", Country = "UK", TotalPrice = 350
            }
        };
    }
}