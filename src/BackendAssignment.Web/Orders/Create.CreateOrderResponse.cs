namespace BackendAssignment.Web.Orders;

public class CreateOrderResponse : BaseResponse
{
  public CreateOrderResponse(Guid correlationId, Guid orderId) : base(correlationId)
  {
    OrderId = orderId;
  }

  public Guid OrderId { get; set; }
}
