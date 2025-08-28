using FluentValidation;

namespace BackendAssignment.Web.Orders;

public class CreateOrderValidator : Validator<CreateOrderRequest>
{
  public CreateOrderValidator()
  {
    RuleFor(x => x.OrderId)
      .NotEmpty()
      .WithMessage("Order ID is required");

    RuleFor(x => x.CustomerName)
      .NotEmpty()
      .WithMessage("Customer name is required")
      .MaximumLength(100)
      .WithMessage("Customer name must be 100 characters or less");

    RuleFor(x => x.OrderItems)
      .NotEmpty()
      .WithMessage("At least one order item is required");

    RuleForEach(x => x.OrderItems)
      .SetValidator(new CreateOrderItemValidator());
  }
}

public class CreateOrderItemValidator : Validator<CreateOrderItem>
{
  public CreateOrderItemValidator()
  {
    RuleFor(x => x.ProductId)
      .GreaterThan(0)
      .WithMessage("Product ID must be valid");

    RuleFor(x => x.Quantity)
      .GreaterThan(0)
      .WithMessage("Quantity must be at least 1");
  }
}
