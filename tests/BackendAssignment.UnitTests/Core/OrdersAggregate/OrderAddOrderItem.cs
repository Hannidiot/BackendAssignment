using BackendAssignment.Core.OrdersAggregate;

namespace BackendAssignment.UnitTests.Core.OrdersAggregate;

public class OrderAddOrderItem
{
  [Fact]
  public void AddsOrderItemToCollection()
  {
    var orderId = Guid.NewGuid();
    var order = new Order(orderId, "Test Customer");
    var productId = 1;
    var quantity = 2;

    order.AddOrderItem(productId, quantity);

    order.OrderItems.Count.ShouldBe(1);
    var orderItem = order.OrderItems.First();
    orderItem.OrderId.ShouldBe(orderId);
    orderItem.ProductId.ShouldBe(productId);
    orderItem.Quantity.ShouldBe(quantity);
  }

  [Fact]
  public void AddsMultipleOrderItemsToCollection()
  {
    var orderId = Guid.NewGuid();
    var order = new Order(orderId, "Test Customer");

    order.AddOrderItem(1, 2);
    order.AddOrderItem(2, 3);
    order.AddOrderItem(3, 1);

    order.OrderItems.Count.ShouldBe(3);
    
    var firstItem = order.OrderItems[0];
    firstItem.ProductId.ShouldBe(1);
    firstItem.Quantity.ShouldBe(2);
    firstItem.OrderId.ShouldBe(orderId);

    var secondItem = order.OrderItems[1];
    secondItem.ProductId.ShouldBe(2);
    secondItem.Quantity.ShouldBe(3);
    secondItem.OrderId.ShouldBe(orderId);

    var thirdItem = order.OrderItems[2];
    thirdItem.ProductId.ShouldBe(3);
    thirdItem.Quantity.ShouldBe(1);
    thirdItem.OrderId.ShouldBe(orderId);
  }
}
