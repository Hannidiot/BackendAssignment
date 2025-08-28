namespace BackendAssignment.Core.OrdersAggregate;

public class Order : EntityBase<Guid>, IAggregateRoot
{
    public string CustomerName { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public List<OrderItem> OrderItems { get; set; } = new();

    public Order() { }

    public Order(Guid id, string customerName)
    {
        Id = id;
        CustomerName = customerName;
        CreatedAt = DateTime.UtcNow;
    }

    public void AddOrderItem(int productId, int quantity)
    {
        OrderItems.Add(new OrderItem(Id, productId, quantity));
    }
}
