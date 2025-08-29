using BackendAssignment.Core.OrdersAggregate;
using BackendAssignment.Core.ProductsAggregate;
using BackendAssignment.Infrastructure.Data;

namespace BackendAssignment.IntegrationTests.Data;

public abstract class BaseEfRepoTestFixture
{
  protected AppDbContext _dbContext;

  protected BaseEfRepoTestFixture()
  {
    var options = CreateNewContextOptions();

    _dbContext = new AppDbContext(options);
  }

  protected static DbContextOptions<AppDbContext> CreateNewContextOptions()
  {
    var serviceProvider = new ServiceCollection()
        .AddEntityFrameworkInMemoryDatabase()
        .BuildServiceProvider();

    var builder = new DbContextOptionsBuilder<AppDbContext>();
    builder.UseInMemoryDatabase("BackendAssignment")
           .UseInternalServiceProvider(serviceProvider);

    return builder.Options;
  }

  protected EfRepository<Order> GetOrderRepository()
  {
    return new EfRepository<Order>(_dbContext);
  }

  protected EfRepository<Product> GetProductRepository()
  {
    return new EfRepository<Product>(_dbContext);
  }
}
