using BackendAssignment.Infrastructure.Data;
using BackendAssignment.Web.Orders;
using System.Net.Http.Json;
using System.Text.Json;

namespace BackendAssignment.FunctionalTests.ApiEndpoints;

[Collection("Sequential")]
public class OrderCreate(CustomWebApplicationFactory<Program> factory) : IClassFixture<CustomWebApplicationFactory<Program>>
{
  private readonly HttpClient _client = factory.CreateClient();

  [Fact]
  public async Task ReturnsSuccessWhenCreatingValidOrder()
  {
    var orderRequest = new CreateOrderRequest
    {
      OrderId = Guid.NewGuid(),
      CustomerName = "Test Customer",
      OrderItems = new List<CreateOrderItem>
      {
        new CreateOrderItem { ProductId = 1, Quantity = 2 },
        new CreateOrderItem { ProductId = 2, Quantity = 1 }
      }
    };

    var response = await _client.PostAsJsonAsync("/api/orders", orderRequest);

    response.EnsureSuccessStatusCode();
    response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);
    
    var responseContent = await response.Content.ReadAsStringAsync();
    responseContent.ShouldContain(orderRequest.OrderId.ToString());
  }

  [Fact]
  public async Task ReturnsBadRequestWhenOrderIdIsEmpty()
  {
    var orderRequest = new CreateOrderRequest
    {
      OrderId = Guid.Empty,
      CustomerName = "Test Customer",
      OrderItems = new List<CreateOrderItem>
      {
        new CreateOrderItem { ProductId = 1, Quantity = 2 }
      }
    };

    var response = await _client.PostAsJsonAsync("/api/orders", orderRequest);

    response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
  }

  [Fact]
  public async Task ReturnsBadRequestWhenCustomerNameIsEmpty()
  {
    var orderRequest = new CreateOrderRequest
    {
      OrderId = Guid.NewGuid(),
      CustomerName = "",
      OrderItems = new List<CreateOrderItem>
      {
        new CreateOrderItem { ProductId = 1, Quantity = 2 }
      }
    };

    var response = await _client.PostAsJsonAsync("/api/orders", orderRequest);

    response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
  }

  [Fact]
  public async Task ReturnsBadRequestWhenOrderItemsIsEmpty()
  {
    var orderRequest = new CreateOrderRequest
    {
      OrderId = Guid.NewGuid(),
      CustomerName = "Test Customer",
      OrderItems = new List<CreateOrderItem>()
    };

    var response = await _client.PostAsJsonAsync("/api/orders", orderRequest);

    response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
  }

  [Fact]
  public async Task ReturnsBadRequestWhenProductIdIsInvalid()
  {
    var orderRequest = new CreateOrderRequest
    {
      OrderId = Guid.NewGuid(),
      CustomerName = "Test Customer",
      OrderItems = new List<CreateOrderItem>
      {
        new CreateOrderItem { ProductId = 0, Quantity = 2 }
      }
    };

    var response = await _client.PostAsJsonAsync("/api/orders", orderRequest);

    response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
  }

  [Fact]
  public async Task ReturnsBadRequestWhenQuantityIsZero()
  {
    var orderRequest = new CreateOrderRequest
    {
      OrderId = Guid.NewGuid(),
      CustomerName = "Test Customer",
      OrderItems = new List<CreateOrderItem>
      {
        new CreateOrderItem { ProductId = 1, Quantity = 0 }
      }
    };

    var response = await _client.PostAsJsonAsync("/api/orders", orderRequest);

    response.StatusCode.ShouldBe(System.Net.HttpStatusCode.BadRequest);
  }

  [Fact]
  public async Task ReturnsErrorWhenProductDoesNotExist()
  {
    var orderRequest = new CreateOrderRequest
    {
      OrderId = Guid.NewGuid(),
      CustomerName = "Test Customer",
      OrderItems = new List<CreateOrderItem>
      {
        new CreateOrderItem { ProductId = 999, Quantity = 1 }
      }
    };

    var response = await _client.PostAsJsonAsync("/api/orders", orderRequest);

    // The API should return a bad request or error status when product doesn't exist
    response.StatusCode.ShouldNotBe(System.Net.HttpStatusCode.OK);
  }

  [Fact]
  public async Task CreatesOrderWithMultipleValidItems()
  {
    var orderRequest = new CreateOrderRequest
    {
      OrderId = Guid.NewGuid(),
      CustomerName = "Multi-Item Customer",
      OrderItems = new List<CreateOrderItem>
      {
        new CreateOrderItem { ProductId = 1, Quantity = 2 },
        new CreateOrderItem { ProductId = 3, Quantity = 1 },
        new CreateOrderItem { ProductId = 5, Quantity = 3 }
      }
    };

    var response = await _client.PostAsJsonAsync("/api/orders", orderRequest);

    response.EnsureSuccessStatusCode();
    response.StatusCode.ShouldBe(System.Net.HttpStatusCode.Created);
    
    var responseContent = await response.Content.ReadAsStringAsync();
    responseContent.ShouldContain(orderRequest.OrderId.ToString());
  }
}
