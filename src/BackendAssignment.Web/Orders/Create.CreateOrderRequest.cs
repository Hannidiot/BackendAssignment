using System.ComponentModel.DataAnnotations;

namespace BackendAssignment.Web.Orders;

public class CreateOrderRequest : BaseRequest
{
  [Required]
  public Guid OrderId { get; set; }
  [Required]
  public string? CustomerName { get; set; }
  
  [Required]
  [MinLength(1, ErrorMessage = "At least one order item is required")]
  public List<CreateOrderItem> OrderItems { get; set; } = new();
}
