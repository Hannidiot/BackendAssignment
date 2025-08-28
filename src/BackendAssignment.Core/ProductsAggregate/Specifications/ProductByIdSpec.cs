namespace BackendAssignment.Core.ProductsAggregate.Specifications;

public class ProductByIdSpec : Specification<Product>
{
  public ProductByIdSpec(int productId) =>
    Query
      .Where(product => product.Id == productId);
}
