namespace BackendAssignment.Core.OrdersAggregate;

public class OrderItem : EntityBase
{
    public Guid OrderId { get; set; }
    public int ProductId { get; set; }
}
