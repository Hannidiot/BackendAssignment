using BackendAssignment.Core.ProductsAggregate;

namespace BackendAssignment.UnitTests.Core.ProductsAggregate;

public class ProductConstructor
{
  [Fact]
  public void InitializesProductWithIdAndName()
  {
    var productId = 1;
    var productName = "Test Product";

    var product = new Product(productId, productName);

    product.Id.ShouldBe(productId);
    product.ProductName.ShouldBe(productName);
  }

  [Fact]
  public void InitializesProductWithParameterlessConstructor()
  {
    var product = new Product();

    product.Id.ShouldBe(0);
    product.ProductName.ShouldBe(string.Empty);
  }

  [Fact]
  public void AllowsUpdatingProductName()
  {
    var product = new Product(1, "Original Name");
    var newName = "Updated Name";

    product.ProductName = newName;

    product.ProductName.ShouldBe(newName);
    product.Id.ShouldBe(1); // Should remain unchanged
  }
}
