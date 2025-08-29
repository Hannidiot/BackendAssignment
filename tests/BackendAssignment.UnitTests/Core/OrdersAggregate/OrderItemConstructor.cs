using BackendAssignment.Core.OrdersAggregate;

namespace BackendAssignment.UnitTests.Core.OrdersAggregate;

public class OrderItemConstructor
{
  [Fact]
  public void InitializesOrderItemWithAllParameters()
  {
    var orderId = Guid.NewGuid();
    var productId = 1;
    var quantity = 5;

    var orderItem = new OrderItem(orderId, productId, quantity);

    orderItem.OrderId.ShouldBe(orderId);
    orderItem.ProductId.ShouldBe(productId);
    orderItem.Quantity.ShouldBe(quantity);
    // Note: ID from EntityBase starts at 0 and is set by the database upon persistence
  }

  [Fact]
  public void InitializesOrderItemWithNoParameters()
  {
    var orderItem = new OrderItem();

    orderItem.OrderId.ShouldBe(Guid.Empty);
    orderItem.ProductId.ShouldBe(0);
    orderItem.Quantity.ShouldBe(0);
  }
}
