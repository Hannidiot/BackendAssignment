using System.ComponentModel.DataAnnotations;

namespace BackendAssignment.Web.Orders;

public class CreateOrderRequest : BaseRequest
{
  [Required]
  public Guid OrderId { get; set; }
  [Required]
  public string? CustomerName { get; set; }
}
