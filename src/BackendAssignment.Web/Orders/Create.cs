
namespace BackendAssignment.Web.Orders;

public class Create(IMediator _mediator)
  : Endpoint<CreateOrderRequest, CreateOrderResponse>
{
  public override void Configure()
  {
    Post("/api/orders");
    AllowAnonymous();
  }

  public override async Task HandleAsync(CreateOrderRequest req, CancellationToken ct)
  {
    
  }
}
