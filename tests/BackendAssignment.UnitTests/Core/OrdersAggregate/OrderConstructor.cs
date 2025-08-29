using BackendAssignment.Core.OrdersAggregate;

namespace BackendAssignment.UnitTests.Core.OrdersAggregate;

public class OrderConstructor
{
  [Fact]
  public void InitializesOrderWithGuidIdAndCustomerName()
  {
    var orderId = Guid.NewGuid();
    var customerName = "Test Customer";

    var order = new Order(orderId, customerName);

    order.Id.ShouldBe(orderId);
    order.CustomerName.ShouldBe(customerName);
    order.CreatedAt.ShouldBeGreaterThan(DateTime.UtcNow.AddSeconds(-1));
    order.OrderItems.ShouldNotBeNull();
    order.OrderItems.Count.ShouldBe(0);
  }

  [Fact]
  public void InitializesOrderWithNoParameters()
  {
    var order = new Order();

    order.Id.ShouldBe(Guid.Empty);
    order.CustomerName.ShouldBe(string.Empty);
    order.OrderItems.ShouldNotBeNull();
    order.OrderItems.Count.ShouldBe(0);
  }
}
