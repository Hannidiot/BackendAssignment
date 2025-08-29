using BackendAssignment.Core.OrdersAggregate;
using BackendAssignment.Core.ProductsAggregate;

namespace BackendAssignment.IntegrationTests.Data;

public class EfRepositoryAdd : BaseEfRepoTestFixture
{
  [Fact]
  public async Task AddsOrder()
  {
    var testCustomerName = "Test Customer";
    var orderId = Guid.NewGuid();
    var repository = GetOrderRepository();
    var order = new Order(orderId, testCustomerName);

    await repository.AddAsync(order);

    var newOrder = (await repository.ListAsync()).FirstOrDefault();

    newOrder.ShouldNotBeNull();
    testCustomerName.ShouldBe(newOrder.CustomerName);
    orderId.ShouldBe(newOrder.Id);
    newOrder.CreatedAt.ShouldBeGreaterThan(DateTime.UtcNow.AddMinutes(-1));
  }

  [Fact]
  public async Task AddsOrderWithOrderItems()
  {
    var testCustomerName = "Test Customer";
    var orderId = Guid.NewGuid();
    var repository = GetOrderRepository();
    var order = new Order(orderId, testCustomerName);
    
    order.AddOrderItem(1, 2);
    order.AddOrderItem(2, 3);

    await repository.AddAsync(order);

    var newOrder = (await repository.ListAsync()).FirstOrDefault();

    newOrder.ShouldNotBeNull();
    newOrder.OrderItems.Count.ShouldBe(2);
    newOrder.OrderItems[0].ProductId.ShouldBe(1);
    newOrder.OrderItems[0].Quantity.ShouldBe(2);
    newOrder.OrderItems[1].ProductId.ShouldBe(2);
    newOrder.OrderItems[1].Quantity.ShouldBe(3);
  }
}
