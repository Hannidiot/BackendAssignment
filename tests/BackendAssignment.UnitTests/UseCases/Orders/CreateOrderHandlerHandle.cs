using BackendAssignment.Core.OrdersAggregate;
using BackendAssignment.Core.ProductsAggregate;
using BackendAssignment.UseCases.Orders.Create;
using NSubstitute;
using Xunit;

namespace BackendAssignment.UnitTests.UseCases.Orders;

public class CreateOrderHandlerHandle
{
    [Fact]
    public async Task ReturnsSuccessWhenOrderIsValid()
    {
        // Arrange
        var orderRepository = Substitute.For<IRepository<Order>>();
        var productRepository = Substitute.For<IRepository<Product>>();
        var logger = Substitute.For<ILogger<CreateOrderHandler>>();
        
        // Mock existing products
        var products = new List<Product>
        {
            new Product(1, "Product 1"),
            new Product(2, "Product 2")
        };
        productRepository.ListAsync(Arg.Any<CancellationToken>()).Returns(products);
        
        var handler = new CreateOrderHandler(orderRepository, productRepository, logger);
        
        var orderItems = new List<OrderItemDto>
        {
            new OrderItemDto(1, 2),
            new OrderItemDto(2, 1)
        };
        
        var command = new CreateOrderCommand(Guid.NewGuid(), "Test Customer", orderItems);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.True(result.IsSuccess);
        await orderRepository.Received(1).AddAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
        await orderRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }
    
    [Fact]
    public async Task ReturnsFailureWhenProductDoesNotExist()
    {
        // Arrange
        var orderRepository = Substitute.For<IRepository<Order>>();
        var productRepository = Substitute.For<IRepository<Product>>();
        var logger = Substitute.For<ILogger<CreateOrderHandler>>();
        
        // Mock existing products (only product 1 exists)
        var products = new List<Product>
        {
            new Product(1, "Product 1")
        };
        productRepository.ListAsync(Arg.Any<CancellationToken>()).Returns(products);
        
        var handler = new CreateOrderHandler(orderRepository, productRepository, logger);
        
        var orderItems = new List<OrderItemDto>
        {
            new OrderItemDto(1, 2),
            new OrderItemDto(999, 1) // Non-existent product
        };
        
        var command = new CreateOrderCommand(Guid.NewGuid(), "Test Customer", orderItems);
        
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        
        // Assert
        Assert.Equal(Ardalis.Result.ResultStatus.Error, result.Status);
        Assert.Contains("Products with IDs 999 do not exist", result.Errors.First());
        await orderRepository.DidNotReceive().AddAsync(Arg.Any<Order>(), Arg.Any<CancellationToken>());
    }
}
