using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendAssignment.Core.OrdersAggregate;
using BackendAssignment.Core.ProductsAggregate;
using BackendAssignment.Core.ProductsAggregate.Specifications;
using BackendAssignment.Core.Interfaces;
using Microsoft.Extensions.Logging;
using Ardalis.Result;

namespace BackendAssignment.UseCases.Orders.Create;

public class CreateOrderHandler : ICommandHandler<CreateOrderCommand, Result<int>>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly ILogger<CreateOrderHandler> _logger;

    public CreateOrderHandler(
        IRepository<Order> orderRepository,
        IRepository<Product> productRepository,
        ILogger<CreateOrderHandler> logger)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _logger = logger;
    }

    public async Task<Result<int>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        try
        {
            // Validate that all products exist
            var productIds = request.OrderItems.Select(oi => oi.ProductId).ToList();
            var products = await _productRepository.ListAsync(cancellationToken);
            
            var missingProductIds = productIds.Except(products.Select(p => p.Id)).ToList();
            if (missingProductIds.Any())
            {
                _logger.LogWarning("Attempted to create order with non-existent products: {MissingProductIds}", missingProductIds);
                return Result<int>.Invalid(new ValidationError($"Products with IDs {string.Join(", ", missingProductIds)} do not exist"));
            }

            // Create the order
            var order = new Order(request.OrderId, request.CustomerName);

            // Add order items
            foreach (var item in request.OrderItems)
            {
                order.AddOrderItem(item.ProductId, item.Quantity);
            }

            // Save the order
            await _orderRepository.AddAsync(order, cancellationToken);
            await _orderRepository.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Order created successfully with ID: {OrderId}", request.OrderId);
            return Result<int>.Success(1); // Return success result
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating order with ID: {OrderId}", request.OrderId);
            return Result<int>.Error($"Error creating order: {ex.Message}");
        }
    }
}
