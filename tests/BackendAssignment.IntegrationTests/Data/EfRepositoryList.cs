using BackendAssignment.Core.OrdersAggregate;
using BackendAssignment.Core.ProductsAggregate;

namespace BackendAssignment.IntegrationTests.Data;

public class EfRepositoryList : BaseEfRepoTestFixture
{
  [Fact]
  public async Task ReturnsEmptyListWhenNoProductsExist()
  {
    var repository = GetProductRepository();

    var products = await repository.ListAsync();

    products.Count.ShouldBe(0);
  }

  [Fact]
  public async Task ReturnsMultipleProducts()
  {
    var repository = GetProductRepository();
    var product1 = new Product(0, "Product 1");
    var product2 = new Product(0, "Product 2");
    var product3 = new Product(0, "Product 3");
    
    await repository.AddAsync(product1);
    await repository.AddAsync(product2);
    await repository.AddAsync(product3);

    var products = await repository.ListAsync();

    products.Count.ShouldBe(3);
    products.ShouldContain(p => p.ProductName == "Product 1");
    products.ShouldContain(p => p.ProductName == "Product 2");
    products.ShouldContain(p => p.ProductName == "Product 3");
  }
}
