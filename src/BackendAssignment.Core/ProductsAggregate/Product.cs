namespace BackendAssignment.Core.ProductsAggregate;

public class Product : EntityBase, IAggregateRoot
{
    public string ProductName { get; set; } = string.Empty;

    public Product() { }

    public Product(int id, string productName)
    {
        Id = id;
        ProductName = productName;
    }
}
