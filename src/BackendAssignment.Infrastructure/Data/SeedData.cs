using BackendAssignment.Core.OrdersAggregate;
using BackendAssignment.Core.ProductsAggregate;

namespace BackendAssignment.Infrastructure.Data;

public static class SeedData
{
  #region Sample Data
  public static readonly List<Product> Products =
  [
    new Product(1, "Laptop"),
    new Product(2, "Smartphone"),
    new Product(3, "Headphones"),
    new Product(4, "Keyboard"),
    new Product(5, "Mouse")
  ];

  public static readonly List<Order> Orders =
  [
    new Order(Guid.NewGuid(), "John Doe")
    {
      OrderItems = new List<OrderItem>
      {
        new OrderItem { ProductId = 1, Quantity = 1 }, // Laptop
        new OrderItem { ProductId = 3, Quantity = 2 }  // Headphones
      }
    },
    new Order(Guid.NewGuid(), "Jane Smith")
    {
      OrderItems = new List<OrderItem>
      {
        new OrderItem { ProductId = 2, Quantity = 1 }, // Smartphone
        new OrderItem { ProductId = 4, Quantity = 1 }, // Keyboard
        new OrderItem { ProductId = 5, Quantity = 1 }  // Mouse
      }
    },
    new Order(Guid.NewGuid(), "Bob Wilson")
    {
      OrderItems = new List<OrderItem>
      {
        new OrderItem { ProductId = 3, Quantity = 3 }, // Headphones
        new OrderItem { ProductId = 5, Quantity = 2 }  // Mouse
      }
    }
  ];
  #endregion

  public static async Task InitializeAsync(AppDbContext dbContext)
  {
    if (await dbContext.Orders.AnyAsync()
      || await dbContext.Products.AnyAsync()) return;

    await PopulateTestDataAsync(dbContext);
  }

  public static async Task PopulateTestDataAsync(AppDbContext dbContext)
  {
    await dbContext.Products.AddRangeAsync(Products);
    await dbContext.Orders.AddRangeAsync(Orders);
    await dbContext.SaveChangesAsync();
  }
}
