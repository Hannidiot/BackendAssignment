using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackendAssignment.UseCases.Orders.Create;

public class CreateOrderCommand : ICommand<Result<Guid>>
{
    public Guid OrderId { get; }
    public string CustomerName { get; }
    public List<OrderItemDto> OrderItems { get; }

    public CreateOrderCommand(Guid orderId, string customerName, List<OrderItemDto> orderItems)
    {
        OrderId = orderId;
        CustomerName = customerName;
        OrderItems = orderItems;
    }
}

public record OrderItemDto(int ProductId, int Quantity);
