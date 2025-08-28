
using BackendAssignment.UseCases.Orders.Create;

namespace BackendAssignment.Web.Orders;

public class Create(IMediator _mediator)
  : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
  public override void Configure()
  {
    Post("/api/orders");
    AllowAnonymous();
    Summary(s =>
    {
      s.Summary = "Create a new order";
      s.Description = "Creates a new order with order items";
      s.ExampleRequest = new CreateOrderRequest 
      { 
        OrderId = Guid.NewGuid(),
        CustomerName = "John Doe",
        OrderItems = new List<CreateOrderItem>
        {
          new CreateOrderItem { ProductId = 1, Quantity = 2 },
          new CreateOrderItem { ProductId = 2, Quantity = 1 }
        }
      };
    });
  }

  public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
  {
    // Convert OrderItemRequest to OrderItemDto
    var orderItems = req.OrderItems.Select(oi => 
      new OrderItemDto(oi.ProductId, oi.Quantity)
    ).ToList();

    var command = new CreateOrderCommand(
      req.OrderId,
      req.CustomerName!,
      orderItems
    );

    var result = await _mediator.Send(command, ct);

    if (result.IsSuccess)
    {
      await SendAsync(new CreateOrderResponse(req.CorrelationId(), result.Value), StatusCodes.Status201Created, ct);
    }
    else
    {
      await SendErrorsAsync(StatusCodes.Status400BadRequest, ct);
    }
  }
}
